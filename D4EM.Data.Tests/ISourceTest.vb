Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data

'''<summary>
'''This is a test class for ISourceTest and is intended
'''to contain all ISourceTest Unit Tests
'''</summary>
<TestClass()> Public Class ISourceTest
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

    Friend Overridable Function CreateISource() As ISource
        Return New D4EM.Data.Source.BASINS
    End Function

    '''<summary>Test for Version</summary>
    <TestMethod()> Public Sub VersionTest()
        Dim lSource As ISource = CreateISource()
        Assert.AreEqual("1.0.0.0", lSource.Version)
    End Sub

    '''<summary>Test for QuerySchema</summary>
    <TestMethod()> Public Sub QuerySchemaTest()
        Dim lSource As ISource = CreateISource()
        Dim lQuerySchema As String = lSource.QuerySchema
        Assert.IsTrue(lQuerySchema.Contains("BASINS"))
    End Sub

    '''<summary>Test for Name</summary>
    <TestMethod()> Public Sub NameTest()
        Dim lSource As ISource = CreateISource()
        Assert.AreEqual("D4EM Data Download::BASINS", lSource.Name)
    End Sub

    '''<summary>Test for Description</summary>
    <TestMethod()> Public Sub DescriptionTest()
        Dim lSource As ISource = CreateISource()
        Assert.AreEqual("Retrieve BASINS data", lSource.Description)
    End Sub

    '''<summary>Test for Author</summary>
    <TestMethod()> Public Sub AuthorTest()
        Dim lSource As ISource = CreateISource()
        Assert.AreEqual("AQUA TERRA Consultants", lSource.Author)
    End Sub

    '''<summary>Test for Execute</summary>
    <TestMethod()> Public Sub ExecuteTest()
        Dim lSource As ISource = CreateISource()
        Dim aQuery As String = lSource.QuerySchema
        Dim lResult As String = lSource.Execute(aQuery)
        Assert.IsTrue(lResult.Contains("xxx"))
    End Sub
End Class
