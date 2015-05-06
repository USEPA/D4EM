Imports atcUtility
Imports System.Collections
Imports DotSpatial.Data
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data

'''<summary>
'''This is a test class for LayerTest and is intended
'''to contain all LayerTest Unit Tests
'''</summary>
<TestClass()> Public Class LayerTest
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

    '''<summary>Test for Specification</summary>
    <TestMethod()> Public Sub SpecificationTest()
        Dim lLayerFileName As String = gProjectFolder & gFilePattern
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Dim lOpenNow As Boolean = False
        Dim lLayer As Layer = New Layer(lLayerFileName, lLayerSpecification, lOpenNow)
        lLayer.Specification = lLayerSpecification
        Assert.AreEqual(lLayerSpecification, lLayer.Specification)
    End Sub

    '''<summary>Test for NeedsProjection</summary>
    <TestMethod()> Public Sub NeedsProjectionTest()
        Dim lLayerFileName As String = gProjectFolder & gFilePattern
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Dim lOpenNow As Boolean = False
        Dim lLayer As Layer = New Layer(lLayerFileName, lLayerSpecification, lOpenNow)
        Assert.AreEqual(True, lLayer.NeedsProjection)
    End Sub

    '''<summary>Test for IsRequired</summary>
    <TestMethod()> Public Sub IsRequiredTest()
        Dim lLayerFileName As String = gProjectFolder & gFilePattern
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Dim lOpenNow As Boolean = False
        Dim lLayer As Layer = New Layer(lLayerFileName, lLayerSpecification, lOpenNow)
        Assert.AreEqual(False, lLayer.IsRequired)
    End Sub

    '''<summary>Test for FileName</summary>
    <TestMethod()> Public Sub FileNameTest()
        Dim lLayerFileName As String = gProjectFolder & gFilePattern
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Dim lOpenNow As Boolean = False
        Dim lLayer As Layer = New Layer(lLayerFileName, lLayerSpecification, lOpenNow)
        Assert.AreEqual(lLayerFileName, lLayer.FileName)
    End Sub

    '''<summary>Test for DataSet</summary>
    <TestMethod()> Public Sub DataSetTest()
        Dim lLayerFileName As String = gProjectFolder & gFilePattern
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Dim lOpenNow As Boolean = False
        Dim lLayer As Layer = New Layer(lLayerFileName, lLayerSpecification, lOpenNow)
        With lLayer.DataSet
            Assert.AreEqual("cat", .Name)
            Assert.AreEqual(DotSpatial.Data.SpaceTimeSupport.Spatial, .SpaceTimeSupport)
        End With
    End Sub

    '''<summary>Test for ReclassifyRange</summary>
    <TestMethod()> Public Sub ReclassifyRangeTest()
        Dim lLayerFileName As String = gProjectFolder & gFilePattern
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Dim lOpenNow As Boolean = False
        Dim lLayer As Layer = New Layer(lLayerFileName, lLayerSpecification, lOpenNow)

        Dim aReclassiflyScheme As Generic.List(Of Double) = Nothing ' TODO: Initialize to an appropriate value
        Dim aReclassifyGridName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = lLayer.ReclassifyRange(aReclassiflyScheme, aReclassifyGridName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for Reclassify</summary>
    <TestMethod()> Public Sub ReclassifyTest()
        Dim lLayerFileName As String = gProjectFolder & gFilePattern
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Dim lOpenNow As Boolean = False
        Dim lLayer As Layer = New Layer(lLayerFileName, lLayerSpecification, lOpenNow)

        Dim aReclassiflyScheme As atcCollection = Nothing ' TODO: Initialize to an appropriate value
        Dim aReclassifyGridName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aNoKeyNoData As Boolean = False ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = lLayer.Reclassify(aReclassiflyScheme, aReclassifyGridName, aNoKeyNoData)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for Open</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.dll")> Public Sub OpenTest()
        Dim lLayerFileName As String = gProjectFolder & gFilePattern
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Dim lOpenNow As Boolean = False
        Dim lLayer As Layer = New Layer(lLayerFileName, lLayerSpecification, lOpenNow)
        Assert.Inconclusive("TODO: Implement code to verify target")

        'Dim lPrivateObject As PrivateObject = New PrivateObject(lLayer)
        'Dim lAccessor As Layer_Accessor = New Layer_Accessor(lPrivateObject)
        'lAccessor.Open()
        'Assert.IsTrue(Not (lAccessor.pDataSet Is Nothing))
    End Sub

    '''<summary>Test for CopyProcStepsFromCachedFile</summary>
    <TestMethod()> Public Sub CopyProcStepsFromCachedFileTest()
        Dim lLayerFileName As String = gProjectFolder & gFilePattern
        Dim lLayerSpecification As LayerSpecification = CreateTestLayerSpecification()
        Dim lOpenNow As Boolean = False
        Dim lLayer As Layer = New Layer(lLayerFileName, lLayerSpecification, lOpenNow)
        Dim lCacheFilename As String = gCacheFolder & gFilePattern
        lLayer.CopyProcStepsFromCachedFile(lCacheFilename)
        Assert.IsTrue(IO.File.Exists(IO.Path.ChangeExtension(lCacheFilename, "xml")))
    End Sub

    '''<summary>Test for CopyProcStepsFromCachedFile</summary>
    <TestMethod()> Public Sub CopyProcStepsFromCachedFileTest1()
        Dim aCacheFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDataFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Layer.CopyProcStepsFromCachedFile(aCacheFilename, aDataFilename)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for Close</summary>
    <TestMethod()> Public Sub CloseTest()
        Dim aLayerFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aLayerSpecification As LayerSpecification = Nothing ' TODO: Initialize to an appropriate value
        Dim aOpenNow As Boolean = False ' TODO: Initialize to an appropriate value
        Dim target As Layer = New Layer(aLayerFileName, aLayerSpecification, aOpenNow) ' TODO: Initialize to an appropriate value
        target.Close()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for AddProcessStepToFile</summary>
    <TestMethod()> Public Sub AddProcessStepToFileTest()
        Dim aProcessStep As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDataFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Layer.AddProcessStepToFile(aProcessStep, aDataFilename)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for AddProcessStep</summary>
    <TestMethod()> Public Sub AddProcessStepTest()
        Dim aLayerFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aLayerSpecification As LayerSpecification = Nothing ' TODO: Initialize to an appropriate value
        Dim aOpenNow As Boolean = False ' TODO: Initialize to an appropriate value
        Dim target As Layer = New Layer(aLayerFileName, aLayerSpecification, aOpenNow) ' TODO: Initialize to an appropriate value
        Dim aProcessStep As String = String.Empty ' TODO: Initialize to an appropriate value
        target.AddProcessStep(aProcessStep)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for Layer Constructor</summary>
    <TestMethod()> Public Sub LayerConstructorTest()
        Dim aLayerFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aLayerSpecification As LayerSpecification = Nothing ' TODO: Initialize to an appropriate value
        Dim aOpenNow As Boolean = False ' TODO: Initialize to an appropriate value
        Dim target As Layer = New Layer(aLayerFileName, aLayerSpecification, aOpenNow)
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
