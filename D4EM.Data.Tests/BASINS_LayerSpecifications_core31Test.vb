Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data.Source

'''<summary>
'''This is a test class for BASINS_LayerSpecifications_core31Test and is intended
'''to contain all BASINS_LayerSpecifications_core31Test Unit Tests
'''</summary>
<TestClass()> Public Class BASINS_LayerSpecifications_core31Test
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

    '''<summary>Test for core31 Constructor</summary>
    <TestMethod()> Public Sub BASINS_LayerSpecifications_core31ConstructorTest()
        Assert.AreEqual(GetType(BASINS), BASINS.LayerSpecifications.core31.cat.Source)
    End Sub
End Class
