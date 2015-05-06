Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data

'''<summary>
'''This is a test class for ZipperTest and is intended
'''to contain all ZipperTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ZipperTest
    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region

    '''<summary>Test for UnzipFile</summary>
    <TestMethod()> Public Sub UnzipFileTest()
        Dim lFileName As String = "GA090219.zip"
        Dim lZipFilename As String = gCacheFolder & "clsBASINS\Met\" & lFileName
        Dim lDestinationFolder As String = gProjectFolder & "met\"
        Dim lIncrementProgressAfter As Boolean = False
        Dim lProgressSameLevel As Boolean = False
        Zipper.UnzipFile(lZipFilename, lDestinationFolder, lIncrementProgressAfter, lProgressSameLevel)
        Dim lActualFileName As String = lDestinationFolder & IO.Path.ChangeExtension(lFileName, "wdm")
        Dim lActualFileInfo As IO.FileInfo = New IO.FileInfo(lActualFileName)
        Assert.AreEqual(CType(2375680, Int64), lActualFileInfo.Length)
        IO.File.Delete(lActualFileName)
    End Sub

    '''<summary>Test for MyExtractProgress</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.dll")> Public Sub MyExtractProgressTest()
        'TODO: need a test here!
        'Dim lArgs As ExtractProgressEventArgs = New Ionic.Zip.ExtractProgressEventArgs
        'lArgs.BytesTransferred = 1000
        'Zipper_Accessor.MyExtractProgress(Nothing, lArgs)
        'Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for Zipper Constructor</summary>
    <TestMethod()> Public Sub ZipperConstructorTest()
        Dim lZipper As Zipper = New Zipper()
        Assert.IsNotNull(lZipper)
        Assert.AreSame(lZipper.GetType, GetType(Zipper))
    End Sub
End Class
