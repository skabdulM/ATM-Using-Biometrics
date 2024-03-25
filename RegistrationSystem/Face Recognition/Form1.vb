Imports System.Diagnostics
Imports Emgu.CV.Structure
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO

Public Class Form1
    Dim currentFrame As Image(Of Bgr, [Byte])
    Dim grabber As Capture
    Dim face As HaarCascade
    Dim eye As HaarCascade
    Dim font As New MCvFont(CvEnum.FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5, 0.5)
    Dim result As Image(Of Gray, Byte), TrainedFace As Image(Of Gray, Byte) = Nothing
    Dim gray As Image(Of Gray, Byte) = Nothing
    Dim trainingImages As New List(Of Image(Of Gray, Byte))()
    Dim labels As New List(Of String)()
    Dim NamePersons As New List(Of String)()
    Dim ContTrain As Integer, NumLabels As Integer, t As Integer
    Dim name As String, names As String = Nothing


    ''' <summary>
    ''' An object recognizer using PCA (Principle Components Analysis)
    ''' </summary>
    <Serializable()> _
    Public Class EigenObjectRecognizer
        Private _eigenImages As Image(Of Gray, [Single])()
        Private _avgImage As Image(Of Gray, [Single])
        Private _eigenValues As Matrix(Of Single)()
        Private _labels As String()
        Private _eigenDistanceThreshold As Double

        ''' <summary>
        ''' Get the eigen vectors that form the eigen space
        ''' </summary>
        ''' <remarks>The set method is primary used for deserialization, do not attemps to set it unless you know what you are doing</remarks>
        Public Property EigenImages() As Image(Of Gray, [Single])()
            Get
                Return _eigenImages
            End Get
            Set(ByVal value As Image(Of Gray, [Single])())
                _eigenImages = value
            End Set
        End Property

        ''' <summary>
        ''' Get or set the labels for the corresponding training image
        ''' </summary>
        Public Property Labels() As [String]()
            Get
                Return _labels
            End Get
            Set(ByVal value As [String]())
                _labels = value
            End Set
        End Property

        ''' <summary>
        ''' Get or set the eigen distance threshold.
        ''' The smaller the number, the more likely an examined image will be treated as unrecognized object. 
        ''' Set it to a huge number (e.g. 5000) and the recognizer will always treated the examined image as one of the known object. 
        ''' </summary>
        Public Property EigenDistanceThreshold() As Double
            Get
                Return _eigenDistanceThreshold
            End Get
            Set(ByVal value As Double)
                _eigenDistanceThreshold = value
            End Set
        End Property

        ''' <summary>
        ''' Get the average Image. 
        ''' </summary>
        ''' <remarks>The set method is primary used for deserialization, do not attemps to set it unless you know what you are doing</remarks>
        Public Property AverageImage() As Image(Of Gray, [Single])
            Get
                Return _avgImage
            End Get
            Set(ByVal value As Image(Of Gray, [Single]))
                _avgImage = value
            End Set
        End Property

        ''' <summary>
        ''' Get the eigen values of each of the training image
        ''' </summary>
        ''' <remarks>The set method is primary used for deserialization, do not attemps to set it unless you know what you are doing</remarks>
        Public Property EigenValues() As Matrix(Of Single)()
            Get
                Return _eigenValues
            End Get
            Set(ByVal value As Matrix(Of Single)())
                _eigenValues = value
            End Set
        End Property

        Private Sub New()
        End Sub


        ''' <summary>
        ''' Create an object recognizer using the specific tranning data and parameters, it will always return the most similar object
        ''' </summary>
        ''' <param name="images">The images used for training, each of them should be the same size. It's recommended the images are histogram normalized</param>
        ''' <param name="termCrit">The criteria for recognizer training</param>
        Public Sub New(ByVal images As Image(Of Gray, [Byte])(), ByRef termCrit As MCvTermCriteria)
            Me.New(images, GenerateLabels(images.Length), termCrit)
        End Sub

        Private Shared Function GenerateLabels(ByVal size As Integer) As [String]()
            Dim labels As [String]() = New String(size - 1) {}
            For i As Integer = 0 To size - 1
                labels(i) = i.ToString()
            Next
            Return labels
        End Function

        ''' <summary>
        ''' Create an object recognizer using the specific tranning data and parameters, it will always return the most similar object
        ''' </summary>
        ''' <param name="images">The images used for training, each of them should be the same size. It's recommended the images are histogram normalized</param>
        ''' <param name="labels">The labels corresponding to the images</param>
        ''' <param name="termCrit">The criteria for recognizer training</param>
        Public Sub New(ByVal images As Image(Of Gray, [Byte])(), ByVal labels As [String](), ByRef termCrit As MCvTermCriteria)
            Me.New(images, labels, 0, termCrit)
        End Sub

        ''' <summary>
        ''' Create an object recognizer using the specific tranning data and parameters
        ''' </summary>
        ''' <param name="images">The images used for training, each of them should be the same size. It's recommended the images are histogram normalized</param>
        ''' <param name="labels">The labels corresponding to the images</param>
        ''' <param name="eigenDistanceThreshold">
        ''' The eigen distance threshold, (0, ~1000].
        ''' The smaller the number, the more likely an examined image will be treated as unrecognized object. 
        ''' If the threshold is &lt; 0, the recognizer will always treated the examined image as one of the known object. 
        ''' </param>
        ''' <param name="termCrit">The criteria for recognizer training</param>
        Public Sub New(ByVal images As Image(Of Gray, [Byte])(), ByVal labels As [String](), ByVal eigenDistanceThreshold As Double, ByRef termCrit As MCvTermCriteria)
            Debug.Assert(images.Length = labels.Length, "The number of images should equals the number of labels")
            Debug.Assert(eigenDistanceThreshold >= 0.0, "Eigen-distance threshold should always >= 0.0")

            CalcEigenObjects(images, termCrit, _eigenImages, _avgImage)

            '
            '         _avgImage.SerializationCompressionRatio = 9;
            '
            '         foreach (Image<Gray, Single> img in _eigenImages)
            '             //Set the compression ration to best compression. The serialized object can therefore save spaces
            '             img.SerializationCompressionRatio = 9;
            '         


            _eigenValues = Array.ConvertAll(Of Image(Of Gray, [Byte]), Matrix(Of Single))(images, Function(img As Image(Of Gray, [Byte])) New Matrix(Of Single)(EigenDecomposite(img, _eigenImages, _avgImage)))

            _labels = labels

            _eigenDistanceThreshold = eigenDistanceThreshold
        End Sub

#Region "static methods"
        ''' <summary>
        ''' Caculate the eigen images for the specific traning image
        ''' </summary>
        ''' <param name="trainingImages">The images used for training </param>
        ''' <param name="termCrit">The criteria for tranning</param>
        ''' <param name="eigenImages">The resulting eigen images</param>
        ''' <param name="avg">The resulting average image</param>
        Public Shared Sub CalcEigenObjects(ByVal trainingImages As Image(Of Gray, [Byte])(), ByRef termCrit As MCvTermCriteria, ByRef eigenImages As Image(Of Gray, [Single])(), ByRef avg As Image(Of Gray, [Single]))
            Dim width As Integer = trainingImages(0).Width
            Dim height As Integer = trainingImages(0).Height

            Dim inObjs As IntPtr() = Array.ConvertAll(Of Image(Of Gray, [Byte]), IntPtr)(trainingImages, Function(img As Image(Of Gray, [Byte])) img.Ptr)

            If termCrit.max_iter <= 0 OrElse termCrit.max_iter > trainingImages.Length Then
                termCrit.max_iter = trainingImages.Length
            End If

            Dim maxEigenObjs As Integer = termCrit.max_iter

            '#Region "initialize eigen images"
            eigenImages = New Image(Of Gray, Single)(maxEigenObjs - 1) {}
            For i As Integer = 0 To eigenImages.Length - 1
                eigenImages(i) = New Image(Of Gray, Single)(width, height)
            Next
            Dim eigObjs As IntPtr() = Array.ConvertAll(Of Image(Of Gray, [Single]), IntPtr)(eigenImages, Function(img As Image(Of Gray, [Single])) img.Ptr)
            '#End Region

            avg = New Image(Of Gray, [Single])(width, height)

            CvInvoke.cvCalcEigenObjects(inObjs, termCrit, eigObjs, Nothing, avg.Ptr)
        End Sub

        ''' <summary>
        ''' Decompose the image as eigen values, using the specific eigen vectors
        ''' </summary>
        ''' <param name="src">The image to be decomposed</param>
        ''' <param name="eigenImages">The eigen images</param>
        ''' <param name="avg">The average images</param>
        ''' <returns>Eigen values of the decomposed image</returns>
        Public Shared Function EigenDecomposite(ByVal src As Image(Of Gray, [Byte]), ByVal eigenImages As Image(Of Gray, [Single])(), ByVal avg As Image(Of Gray, [Single])) As Single()
            Return CvInvoke.cvEigenDecomposite(src.Ptr, Array.ConvertAll(Of Image(Of Gray, [Single]), IntPtr)(eigenImages, Function(img As Image(Of Gray, [Single])) img.Ptr), avg.Ptr)
        End Function
#End Region

        ''' <summary>
        ''' Given the eigen value, reconstruct the projected image
        ''' </summary>
        ''' <param name="eigenValue">The eigen values</param>
        ''' <returns>The projected image</returns>
        Public Function EigenProjection(ByVal eigenValue As Single()) As Image(Of Gray, [Byte])
            Dim res As Image(Of Gray, [Byte]) = New Image(Of Gray, Byte)(_avgImage.Width, _avgImage.Height)
            CvInvoke.cvEigenProjection(Array.ConvertAll(Of Image(Of Gray, [Single]), IntPtr)(_eigenImages, Function(img As Image(Of Gray, [Single])) img.Ptr), eigenValue, _avgImage.Ptr, res.Ptr)
            Return res
        End Function

        ''' <summary>
        ''' Get the Euclidean eigen-distance between <paramref name="image"/> and every other image in the database
        ''' </summary>
        ''' <param name="image">The image to be compared from the training images</param>
        ''' <returns>An array of eigen distance from every image in the training images</returns>
        Public Function GetEigenDistances(ByVal image As Image(Of Gray, [Byte])) As Single()
            Using eigenValue As New Matrix(Of Single)(EigenDecomposite(image, _eigenImages, _avgImage))
                Return Array.ConvertAll(Of Matrix(Of Single), Single)(_eigenValues, Function(eigenValueI As Matrix(Of Single)) CSng(CvInvoke.cvNorm(eigenValue.Ptr, eigenValueI.Ptr, Emgu.CV.CvEnum.NORM_TYPE.CV_L2, IntPtr.Zero)))
            End Using
        End Function

        ''' <summary>
        ''' Given the <paramref name="image"/> to be examined, find in the database the most similar object, return the index and the eigen distance
        ''' </summary>
        ''' <param name="image">The image to be searched from the database</param>
        ''' <param name="index">The index of the most similar object</param>
        ''' <param name="eigenDistance">The eigen distance of the most similar object</param>
        ''' <param name="label">The label of the specific image</param>
        Public Sub FindMostSimilarObject(ByVal image As Image(Of Gray, [Byte]), ByRef index As Integer, ByRef eigenDistance As Single, ByRef label As [String])
            Dim dist As Single() = GetEigenDistances(image)

            index = 0
            eigenDistance = dist(0)
            For i As Integer = 1 To dist.Length - 1
                If dist(i) < eigenDistance Then
                    index = i
                    eigenDistance = dist(i)
                End If
            Next
            label = Labels(index)
        End Sub

        ''' <summary>
        ''' Try to recognize the image and return its label
        ''' </summary>
        ''' <param name="image">The image to be recognized</param>
        ''' <returns>
        ''' String.Empty, if not recognized;
        ''' Label of the corresponding image, otherwise
        ''' </returns>
        Public Function Recognize(ByVal image As Image(Of Gray, [Byte])) As [String]
            Dim index As Integer
            Dim eigenDistance As Single
            Dim label As [String]
            FindMostSimilarObject(image, index, eigenDistance, label)

            Return If((_eigenDistanceThreshold <= 0 OrElse eigenDistance < _eigenDistanceThreshold), _labels(index), [String].Empty)
        End Function
    End Class

    Private Sub label3_Click(sender As Object, e As EventArgs) Handles label3.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New()
        InitializeComponent()
        'Load haarcascades for face detection
        face = New HaarCascade("haarcascade_frontalface_default.xml")
        'eye = new HaarCascade("haarcascade_eye.xml");
        Try
            'Load of previus trainned faces and labels for each image
            Dim Labelsinfo As String = File.ReadAllText("D:\MiniProject\TrainedFaces\TrainedLabels.txt")
            Dim Labels__1 As String() = Labelsinfo.Split("%"c)
            NumLabels = Convert.ToInt16(Labels__1(0))
            ContTrain = NumLabels
            Dim LoadFaces As String

            For tf As Integer = 1 To NumLabels
                LoadFaces = "face" & tf & ".bmp"
                trainingImages.Add(New Image(Of Gray, Byte)("D:\MiniProject\TrainedFaces\" & LoadFaces))
                labels.Add(Labels__1(tf))

            Next
        Catch e As Exception
            'MessageBox.Show(e.ToString());
            MessageBox.Show("Nothing in database, please add at least a face.", "Triained faces load", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try
    End Sub

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        grabber = New Capture()
        grabber.QueryFrame()
        Timer1.Start()
        button1.Enabled = False
    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button2.Click
        Try
            'Trained face counter
            ContTrain = ContTrain + 1

            'Get a gray frame from capture device
            gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC)

            'Face Detector
            Dim facesDetected As MCvAvgComp()() = gray.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, New Size(20, 20))

            'Action for each element detected
            For Each f As MCvAvgComp In facesDetected(0)
                TrainedFace = currentFrame.Copy(f.rect).Convert(Of Gray, Byte)()
                Exit For
            Next

            'resize face detected image for force to compare the same size with the 
            'test image with cubic interpolation type method
            TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC)
            trainingImages.Add(TrainedFace)
            labels.Add(textBox1.Text)

            'Show face added in gray scale
            imageBox1.Image = TrainedFace

            'Write the number of triained faces in a file text for further load
            File.WriteAllText("D:\MiniProject\TrainedFaces\TrainedLabels.txt", trainingImages.ToArray().Length.ToString() & "%")

            'Write the labels of triained faces in a file text for further load
            For i As Integer = 1 To trainingImages.ToArray().Length
                trainingImages.ToArray()(i - 1).Save("D:\MiniProject\TrainedFaces\face" & i & ".bmp")
                File.AppendAllText("D:\MiniProject\TrainedFaces\TrainedLabels.txt", labels.ToArray()(i - 1) + "%")
            Next

            MessageBox.Show(textBox1.Text + "´s face detected and added :)", "Training OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            textBox1.Text = ""
            imageBox1.Image = Nothing
        Catch
            MessageBox.Show("Enable the face detection first", "Training Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        label3.Text = "0"

        NamePersons.Add("")


        'Get the current frame form capture device
        currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC)

        'Convert it to Grayscale
        gray = currentFrame.Convert(Of Gray, [Byte])()

        'Face Detector
        Dim facesDetected As MCvAvgComp()() = gray.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, New Size(20, 20))

        'Action for each element detected
        For Each f As MCvAvgComp In facesDetected(0)
            t = t + 1
            result = currentFrame.Copy(f.rect).Convert(Of Gray, Byte)().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC)
            'draw the face detected in the 0th (gray) channel with blue color
            currentFrame.Draw(f.rect, New Bgr(Color.Red), 2)


            If trainingImages.ToArray().Length <> 0 Then
                'TermCriteria for face recognition with numbers of trained images like maxIteration
                Dim termCrit As New MCvTermCriteria(ContTrain, 0.001)

                'Eigen face recognizer
                Dim recognizer As New EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 3000, termCrit)

                name = recognizer.Recognize(result)

                'Draw the label for each face detected and recognized

                currentFrame.Draw(name, font, New Point(f.rect.X - 2, f.rect.Y - 2), color:=New Bgr(Color.LightGreen))
            End If

            NamePersons(t - 1) = name
            NamePersons.Add("")

            label3.Text = facesDetected(0).Length.ToString()
        Next
        t = 0

        'Names concatenation of persons recognized
        For nnn As Integer = 0 To facesDetected(0).Length - 1
            names = names + NamePersons(nnn) + ", "
        Next
        'Show the faces procesed and recognized
        imageBoxFrameGrabber.Image = currentFrame
        label4.Text = names
        names = ""
        'Clear the list(vector) of names
        NamePersons.Clear()
    End Sub

End Class
