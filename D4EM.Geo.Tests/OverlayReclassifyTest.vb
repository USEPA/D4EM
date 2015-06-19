Imports atcUtility
Imports System.Text
Imports System.Collections.Generic
Imports D4EM.Data
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Geo

'''<summary>
'''This is a test class for OverlayReclassifyTest and is intended
'''to contain all OverlayReclassifyTest Unit Tests
'''</summary>
<TestClass()> Public Class OverlayReclassifyTest

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

    '''<summary>Test for OverlayReclassify Constructor</summary>
    <TestMethod()> Public Sub OverlayReclassifyConstructorTest()
        Dim target As OverlayReclassify = New OverlayReclassify()
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub

    '''<summary>Test for Overlay</summary>
    <TestMethod()> Public Sub OverlayTest()
        Dim aGridOutputFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aGridSlopeValueFileName As D4EM.Data.Layer = Nothing ' TODO: Initialize to an appropriate value
        Dim aResume As Boolean = False ' TODO: Initialize to an appropriate value
        Dim aLayers() As Layer = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As HRUTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As HRUTable
        actual = OverlayReclassify.Overlay(aGridOutputFileName, aGridSlopeValueFileName, aResume, aLayers)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for Overlay</summary>
    <TestMethod()> Public Sub OverlayTest1()
        Dim aGridOutputFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aGridSlopeValue As Layer = Nothing ' TODO: Initialize to an appropriate value
        Dim aLayers As List(Of Layer) = Nothing ' TODO: Initialize to an appropriate value
        Dim aResume As Boolean = False ' TODO: Initialize to an appropriate value
        Dim expected As HRUTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As HRUTable
        actual = OverlayReclassify.Overlay(aGridOutputFileName, aGridSlopeValue, aLayers, aResume)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for ReportByTag</summary>
    <TestMethod()> Public Sub ReportByTagTest()
        Dim aReport As StringBuilder = Nothing ' TODO: Initialize to an appropriate value
        Dim aCollection As atcCollection = Nothing ' TODO: Initialize to an appropriate value
        Dim aDisplayTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim aDisplayFirst As Boolean = False ' TODO: Initialize to an appropriate value
        Dim aDisplayAll As Boolean = False ' TODO: Initialize to an appropriate value
        Dim aDisplayPredominant As Boolean = False ' TODO: Initialize to an appropriate value
        OverlayReclassify.ReportByTag(aReport, aCollection, aDisplayTags, aDisplayFirst, aDisplayAll, aDisplayPredominant)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for Simplify</summary>
    <TestMethod()> Public Sub SimplifyTest()
        Dim aLayerTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim aSubBasinTable As atcCollection = Nothing ' TODO: Initialize to an appropriate value
        Dim aTag As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aIgnoreBelowFraction As Double = 0.0! ' TODO: Initialize to an appropriate value
        Dim aIgnoreBelowAbsolute As Double = 0.0! ' TODO: Initialize to an appropriate value
        Dim aGridOverlayFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As HRUTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As HRUTable
        actual = OverlayReclassify.Simplify(aLayerTags, aSubBasinTable, aTag, aIgnoreBelowFraction, aIgnoreBelowAbsolute, aGridOverlayFileName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for UniqueValuesSummary</summary>
    <TestMethod()> Public Sub UniqueValuesSummaryTest()
        Dim aHruTable As HRUTable = Nothing ' TODO: Initialize to an appropriate value
        Dim aTag As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = OverlayReclassify.UniqueValuesSummary(aHruTable, aTag)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
