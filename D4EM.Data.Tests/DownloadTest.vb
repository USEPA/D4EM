Imports WorldWind.Net
Imports System.IO
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data

'''<summary>
'''This is a test class for DownloadTest and is intended
'''to contain all DownloadTest Unit Tests
'''</summary>
<TestClass()> Public Class DownloadTest
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
            testContextInstance = value
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

    '''<summary>Test for GetHTTPStreamReader</summary>
    <TestMethod()> Public Sub GetHTTPStreamReaderTest()
        Dim lURL As String = "http://www.epa.gov/athens/"
        Dim lSecondsToRespond As Long = 20
        Dim lResult As StreamReader = Download.GetHTTPStreamReader(lURL, lSecondsToRespond)
        Dim lResultString As String = lResult.ReadToEnd
        Assert.IsTrue(lResultString.Contains("Ecosystems Research Division"))
    End Sub

    '''<summary>Test for DownloadURL</summary>
    <TestMethod()> Public Sub DownloadURLTest()
        Dim lURL As String = "http://www.epa.gov/athens/"
        Dim lSaveAs As String = IO.Path.GetTempFileName
        Dim lResult As Boolean = Download.DownloadURL(lURL, lSaveAs)
        Assert.IsTrue(lResult)
        Assert.IsTrue(IO.File.ReadAllText(lSaveAs).Contains("Ecosystems Research Division"))
        IO.File.Delete(lSaveAs)
    End Sub

    '''<summary>Test for DownloadURL</summary>
    <TestMethod()> Public Sub DownloadURLTest1()
        Dim lURL As String = "http://www.epa.gov/athens/"
        Dim lResultString As String = Download.DownloadURL(lURL)
        Assert.IsTrue(lResultString.Contains("Ecosystems Research Division"))
    End Sub

    '''<summary>Test for DefaultProgressHandler</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.dll")> Public Sub DefaultProgressHandlerTest()
        'TODO: need to actually test something here
        Dim lBytesRead As Integer = 0
        Dim lTotalBytes As Integer = 0
        Download_Accessor.DefaultProgressHandler(lBytesRead, lTotalBytes)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for DefaultCompleteHandler'</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.dll")> Public Sub DefaultCompleteHandlerTest()
        Dim lDownloadInfo As New WebDownload
        Download_Accessor.DefaultCompleteHandler(lDownloadInfo)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub
End Class
