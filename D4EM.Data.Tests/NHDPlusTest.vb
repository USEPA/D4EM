Imports DotSpatial.Projections
Imports System.Xml
Imports D4EM.Data
Imports System.Collections
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data.Source

'''<summary>
'''This is a test class for NHDPlusTest and is intended
'''to contain all NHDPlusTest Unit Tests
'''</summary>
<TestClass()> _
Public Class NHDPlusTest
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

    '''<summary>Test for QuerySchema</summary>
    <TestMethod()> Public Sub QuerySchemaTest()
        Dim lNHDPlus As NHDPlus = New NHDPlus()
        Dim lQuerySchema As String = lNHDPlus.QuerySchema
        Assert.IsTrue(lQuerySchema.Contains("<DataExtension name='NHDPlus'>"))
        Assert.IsTrue(lQuerySchema.Contains("<function name='GetNHDPlus'"))
    End Sub

    '''<summary>Test for Name</summary>
    <TestMethod()> Public Sub NameTest()
        Dim lNHDPlus As NHDPlus = New NHDPlus()
        Dim lName As String = lNHDPlus.Name
        Assert.AreEqual("D4EM Data Download::NHDPlus", lName)
    End Sub

    '''<summary>Test for Description</summary>
    <TestMethod()> Public Sub DescriptionTest()
        Dim lNHDPlus As NHDPlus = New NHDPlus()
        Dim lDescription As String = lNHDPlus.Description
        Assert.AreEqual("Retrieve NHD Plus data", lDescription)
    End Sub

    '''<summary>Test for ShouldMoveNonGrid</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NHDPlus.dll")> Public Sub ShouldMoveNonGridTest()
        Dim aCacheFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDataTypes As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim aESRIgridFolders As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = NHDPlus_Accessor.ShouldMoveNonGrid(aCacheFilename, aESRIgridFolders, aDataTypes)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for JoinAndSaveDbf</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NHDPlus.dll")> Public Sub JoinAndSaveDbfTest()
        Dim aBaseFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aAddFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aBaseFieldName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aAddFieldName As String = String.Empty ' TODO: Initialize to an appropriate value
        NHDPlus_Accessor.JoinAndSaveDbf(aBaseFileName, aAddFileName, aBaseFieldName, aAddFieldName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for GetNHDPlus</summary>
    <TestMethod()> Public Sub GetNHDPlusTest()
        Dim lProject As Project = CreateTestProject()
        If Not gForceDownload Then lProject.CacheFolder = gCacheFolder
        Dim lJoinAttributes As Boolean = True
        Dim lResult As String = NHDPlus.GetNHDPlus(lProject, "NHDPlus", gHuc8, lJoinAttributes)
        Assert.IsTrue(lResult.Contains("<add_shape>"))
        Assert.AreEqual(37, lResult.Split("<add_shape>").Count)
    End Sub

    '''<summary>Test for Execute</summary>
    <TestMethod()> Public Sub ExecuteTest()
        Dim lNHDPlus As NHDPlus = New NHDPlus() ' TODO: Initialize to an appropriate value
        Dim lQuery As String = String.Empty
        Dim lActual = lNHDPlus.Execute(lQuery)
        Assert.AreEqual("<error>Root element is missing.</error>", lActual)
        'TODO: add more tests with a query string
    End Sub

    '''<summary>Test for NHDPlus Constructor</summary>
    <TestMethod()> Public Sub NHDPlusConstructorTest()
        Dim lNHDPlus As NHDPlus = New NHDPlus()
        Assert.AreEqual("D4EM Data Download::NHDPlus", lNHDPlus.Name)
    End Sub

    '''<summary>Test for ClipProjectGrid</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NHDPlus.dll")> Public Sub ClipProjectGridTest()
        'Dim aDesiredProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        'Dim aSourceDir As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aBaseName As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aDestDir As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aClipRegion As Region = Nothing ' TODO: Initialize to an appropriate value
        'Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim actual As String
        'actual = NHDPlus_Accessor.ClipProjectGrid(aDesiredProjection, aSourceDir, aBaseName, aDestDir, aClipRegion)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
