Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Geo

'''<summary>
'''This is a test class for HRUTest and is intended
'''to contain all HRUTest Unit Tests
'''</summary>
<TestClass()> _
Public Class HRUTest

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

    '''<summary>Test for HRU Constructor</summary>
    <TestMethod()> Public Sub HRUConstructorTest()
        Dim aKey As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aHandle As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim aCellCount As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim aIds As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim aArea As Double = 0.0! ' TODO: Initialize to an appropriate value
        Dim aSlopeMean As Double = 0.0! ' TODO: Initialize to an appropriate value
        Dim target As HRU = New HRU(aKey, aHandle, aCellCount, aIds, aArea, aSlopeMean)
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
 
    '''<summary>Test for SetKeyFromIds</summary>
    <TestMethod()> Public Sub SetKeyFromIdsTest()
        Dim aHru As HRU = Nothing ' TODO: Initialize to an appropriate value
        'target.SetKeyFromIds()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub
End Class
