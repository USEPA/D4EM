Imports System.Xml
Imports System.Net
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data.Source

'''<summary>
'''This is a test class for modIDATest and is intended
'''to contain all modIDATest Unit Tests
'''</summary>
<TestClass()> Public Class modIDATest
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

    '''<summary>Test for SaveResponse</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> _
    Public Sub SaveResponseTest()
        Dim lResponse As HttpWebResponse = Nothing ' TODO: Initialize to an appropriate value
        Dim aFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = NWIS_Accessor.SaveResponse(lResponse, aFilename)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for RequestWithHeadersAndCookies</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub RequestWithHeadersAndCookiesTest()
        Dim Request As HttpWebRequest = Nothing ' TODO: Initialize to an appropriate value
        Dim aResponse As HttpWebResponse = Nothing ' TODO: Initialize to an appropriate value
        Dim aResponseExpected As HttpWebResponse = Nothing ' TODO: Initialize to an appropriate value
        Dim aPost As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = NWIS_Accessor.RequestWithHeadersAndCookies(Request, aResponse, aPost)
        Assert.AreEqual(aResponseExpected, aResponse)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
