Module modUtility
    Friend gBaseFolder = atcUtility.AbsolutePath("..\..\..\D4EM.Data.Tests\", IO.Directory.GetCurrentDirectory)
    Friend gHuc8 As String = "03070103"
    Friend gProjectFolder As String = gBaseFolder & "data\" & gHuc8 & "\"
    Friend gCacheFolder As String = gBaseFolder & "cache\"
    Friend gForceDownload As Boolean = False

    Friend Function CreateTestProject() As D4EM.Data.Project
        Dim lDesiredProjection As DotSpatial.Projections.ProjectionInfo = New DotSpatial.Projections.ProjectionInfo("+proj=aea +lat_1=29.5 +lat_2=45.5 +lat_0=23 +lon_0=-96 +x_0=0 +y_0=0 +ellps=GRS80 +datum=NAD83 +units=m +no_defs")
        Dim lRegion As Region = New Region(Region.RegionTypes.huc8, gHuc8)
        Dim lClip As Boolean = False
        Dim lMerge As Boolean = False
        Dim lGetEvenIfCached As Boolean = False
        Dim lCacheOnly As Boolean = False
        Return New Project(lDesiredProjection, gCacheFolder, gProjectFolder, lRegion, lClip, lMerge, lGetEvenIfCached, lCacheOnly)
    End Function

    Friend gFilePattern As String = "cat.shp"
    Friend gTag As String = "core31.cat"
    Friend gRole As D4EM.Data.LayerSpecification.Roles = D4EM.Data.LayerSpecification.Roles.SubBasin
    Friend gIdFieldName As String = "CU"
    Friend gName As String = "Cataloging Unit Boundaries"
    Friend gSource As Type = GetType(D4EM.Data.Source.BASINS)

    Friend Function CreateTestLayerSpecification() As LayerSpecification
        Dim lLayerSpecifcation As LayerSpecification = New LayerSpecification(
           FilePattern:=gFilePattern,
           Tag:=gTag,
           Role:=gRole,
           IdFieldName:=gIdFieldName,
           Name:=gName,
           Source:=gSource)
        Return lLayerSpecifcation
    End Function
End Module
