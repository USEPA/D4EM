Imports atcUtility
Imports System
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Geo

'''<summary>
'''This is a test class for HRUTableTest and is intended
'''to contain all HRUTableTest Unit Tests
'''</summary>
<TestClass()> Public Class HRUTableTest

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

    '''<summary>Test for HRUTable Constructor</summary>
    <TestMethod()> Public Sub HRUTableConstructorTest()
        Dim aFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aFilename)
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub

    '''<summary>Test for HRUTable Constructor</summary>
    <TestMethod()> Public Sub HRUTableConstructorTest1()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags)
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub

    '''<summary>Test for CompareTo</summary>
    <TestMethod()> Public Sub CompareToTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As IComparable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        Dim obj As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.CompareTo(obj)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for ComputeTotalCellCount</summary>
    <TestMethod()> Public Sub ComputeTotalCellCountTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        target.ComputeTotalCellCount()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for CountCellsPerTagValue</summary>
    <TestMethod()> Public Sub CountCellsPerTagValueTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        Dim aTag As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As atcCollection = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As atcCollection
        actual = target.CountCellsPerTagValue(aTag)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for EnsureUnique</summary>
    <TestMethod()> Public Sub EnsureUniqueTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        target.EnsureUnique()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for Id</summary>
    <TestMethod()> Public Sub IdTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        Dim aHru As HRU = Nothing ' TODO: Initialize to an appropriate value
        Dim aTag As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.Id(aHru, aTag)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for PredominantTagValue</summary>
    <TestMethod()> Public Sub PredominantTagValueTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        Dim aTag As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.PredominantTagValue(aTag)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for ReadReclassifyCSV</summary>
    <TestMethod()> Public Sub ReadReclassifyCSVTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        Dim aCsvFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aOriginalIDs As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim aOriginalIDsExpected As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim aNewIds As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim aNewIdsExpected As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim aRemoveAfter As String = String.Empty ' TODO: Initialize to an appropriate value
        target.ReadReclassifyCSV(aCsvFileName, aOriginalIDs, aNewIds, aRemoveAfter)
        Assert.AreEqual(aOriginalIDsExpected, aOriginalIDs)
        Assert.AreEqual(aNewIdsExpected, aNewIds)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for Reclassify</summary>
    <TestMethod()> Public Sub ReclassifyTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        Dim aTag As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aChangeFromValues As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim aChangeToValues As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        target.Reclassify(aTag, aChangeFromValues, aChangeToValues)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for Save</summary>
    <TestMethod()> Public Sub SaveTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        Dim aFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        target.Save(aFilename)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for Sort</summary>
    <TestMethod()> Public Sub SortTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        Dim aDescending As Boolean = False ' TODO: Initialize to an appropriate value
        Dim aTag As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As HRUTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As HRUTable
        actual = target.Sort(aDescending, aTag)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for SplitByTag</summary>
    <TestMethod()> Public Sub SplitByTagTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        Dim aTag As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As atcCollection = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As atcCollection
        actual = target.SplitByTag(aTag)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for SummarizeByTag</summary>
    <TestMethod()> Public Sub SummarizeByTagTest()
        Dim aTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim target As HRUTable = New HRUTable(aTags) ' TODO: Initialize to an appropriate value
        Dim aSortTags As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As atcCollection = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As atcCollection
        actual = target.SummarizeByTag(aSortTags)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
