Imports System
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data

'''<summary>
'''This is a test class for LayerSpecificationTest and is intended
'''to contain all LayerSpecificationTest Unit Tests
'''</summary>
<TestClass()> Public Class LayerSpecificationTest
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

    '''<summary>Test for Tag</summary>
    <TestMethod()> Public Sub TagTest()
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Assert.AreEqual(gTag, lLayerSpecification.Tag)
    End Sub

    '''<summary>Test for Source</summary>
    <TestMethod()> Public Sub SourceTest()
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Assert.AreEqual(gSource, lLayerSpecification.Source)
    End Sub

    '''<summary>Test for Role</summary>
    <TestMethod()> Public Sub RoleTest()
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Assert.AreEqual(gRole, lLayerSpecification.Role)
    End Sub

    '''<summary>Test for NoData</summary>
    <TestMethod()> Public Sub NoDataTest()
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Assert.AreEqual(String.Empty, lLayerSpecification.NoData)
    End Sub

    '''<summary>Test for Name</summary>
    <TestMethod()> Public Sub NameTest()
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Assert.AreEqual(gName, lLayerSpecification.Name)
    End Sub

    '''<summary>Test for IdFieldName</summary>
    <TestMethod()> Public Sub IdFieldNameTest()
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Assert.AreEqual(gIdFieldName, lLayerSpecification.IdFieldName)
    End Sub

    '''<summary>Test for FilePattern</summary>
    <TestMethod()> Public Sub FilePatternTest()
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Assert.AreEqual(gFilePattern, lLayerSpecification.FilePattern)
    End Sub

    '''<summary>Test for op_Inequality</summary>
    <TestMethod()> Public Sub op_InequalityTest()
        Dim lLayerSpecification_1 As LayerSpecification = CreateTestLayerSpecification()
        Dim lLayerSpecification_2 As LayerSpecification = CreateTestLayerSpecification()
        lLayerSpecification_2.Name = "NewName"
        Dim lCompare As Boolean = (lLayerSpecification_1 <> lLayerSpecification_2)
        Assert.AreEqual(True, lCompare)
    End Sub

    '''<summary>Test for op_Equality</summary>
    <TestMethod()> Public Sub op_EqualityTest()
        Dim lLayerSpecification_1 As LayerSpecification = CreateTestLayerSpecification()
        Dim lCompare As Boolean = (lLayerSpecification_1 = lLayerSpecification_1)
        Assert.AreEqual(True, lCompare)
    End Sub

    '''<summary>Test for FromFilename</summary>
    <TestMethod()> Public Sub FromFilenameTest()
        Dim lLayerSpecification_1 As LayerSpecification = LayerSpecification.FromFilename(gFilePattern, gSource)
        Dim lLayerSpecification_2 As LayerSpecification = CreateTestLayerSpecification()
        Assert.AreEqual(lLayerSpecification_2.Name, lLayerSpecification_1.Name)
    End Sub

    '''<summary>Test for LayerSpecification Constructor</summary>
    <TestMethod()> Public Sub LayerSpecificationConstructorTest()
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Assert.IsNotNull(lLayerSpecification)
        'TODO: need better tests here?
    End Sub
End Class
