using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using DotSpatial.Topology;
using MapWinUtility;
using atcUtility;

namespace SDMProjectBuilder
{
    public partial class frmEditLocalData : Form
    {
        private AppManager _appMgr = null;
        private DataTable _dt = null;
        private AddShapeFunction _addShapeFunction = null;
        private MoveVertexFunction _moveVertexFunction = null;

        public frmEditLocalData(AppManager appMgr)
        {
            InitializeComponent();
            _appMgr = appMgr;
        }

        private void frmEditLocalData_Load(object sender, EventArgs e)
        {
            SetButtonsState(false); 
            lstboxDataFiles.SelectedIndex = 0;
        }

        private void SetButtonsState(bool bval)
        {
            btnOpenFile.Enabled = !bval;
            btnEditFile.Enabled = bval;
            btnAddPoint.Enabled = bval;
            btnDeleteSelected.Enabled = bval;
            btnCloseFile.Enabled = bval;
            lstboxDataFiles.Enabled = !bval;

            btnClose.Enabled = !bval;
        }


        private void btnOpenFile_Click(object sender, EventArgs e)
        {            
            SetButtonsState(true);
            
            string fileName = lstboxDataFiles.SelectedItem.ToString();

            //Remove the layer if it had previously been added
            IMapLayer ptLayer = null;
            foreach (Layer layer in _appMgr.Map.Layers)
            {
                if (layer != null)
                {
                    if (layer.DataSet != null)
                    {
                        if (!string.IsNullOrWhiteSpace(layer.DataSet.Name))
                        {
                            if (string.Compare(layer.DataSet.Name, fileName, true) == 0)
                            {
                                ptLayer = layer as IMapLayer;
                                break;
                            }
                        }                            
                    }
                }
            }

            if (ptLayer != null)
            {
                _appMgr.Map.MapFrame.SuspendEvents();
                _appMgr.Map.Layers.Remove(ptLayer);
                _appMgr.Map.MapFrame.ResumeEvents();
                Application.DoEvents();
            }
            string fileNameWithExt = Path.ChangeExtension(fileName, "csv");
            string fileLoc = Path.Combine(_appMgr.SerializationManager.CurrentProjectDirectory, "LocalData");
            ImportLocalData ild = new ImportLocalData(_appMgr);
            FeatureSet fs = ild.LoadShapefileFromCSV(fileNameWithExt, fileLoc);
            fileNameWithExt = Path.ChangeExtension(fileName, "shp");
            fs.SaveAs(Path.Combine(fileLoc, fileNameWithExt), true);
            IMapFeatureLayer iMFL = _appMgr.Map.Layers.Add(fs);
            _appMgr.Map.Layers.SelectedLayer = iMFL;
            iMFL.IsSelected = true;
            _appMgr.Map.ViewExtents = iMFL.Extent;            
        }

        private void btnEditFile_Click(object sender, EventArgs e)
        {
            string fileName = lstboxDataFiles.SelectedItem.ToString();
            IFeatureLayer layer = GetLayer(fileName);
            if (layer != null && layer.DataSet != null)
                layer.ShowAttributes();

        }

        private IFeatureLayer GetLayer(string layerName)
        {
            IFeatureLayer returnLayer = null;
            foreach (IMapFeatureLayer layer in _appMgr.Map.Layers)
            {
                string name = layer.DataSet.Name;
                if (string.IsNullOrWhiteSpace(name))
                    continue;

                if (string.Compare(name, layerName, true) == 0)
                {
                    returnLayer = layer;
                    break;
                }
            }

            return returnLayer;
        }

        private void StartAddPoints()
        {
            Map map = _appMgr.Map as Map;
            try
            {
                string fileName = lstboxDataFiles.SelectedItem.ToString();
                IFeatureLayer layer = GetLayer(fileName);
                if (layer == null)
                {
                    Logger.Msg("Unable to find layer on map: " + fileName, Microsoft.VisualBasic.MsgBoxStyle.Critical, "Could Not Add Point");                    
                    return;
                }

                if (layer.DataSet == null)
                {
                    Logger.Msg("Layer does not have an associated dataset:" + fileName, Microsoft.VisualBasic.MsgBoxStyle.Critical, "Could Not Add Point");                    
                    return;
                }

                //btnAddPoint.Enabled = false;
                
                map.FunctionMode = FunctionMode.None;
                map.Cursor = Cursors.Hand;
                map.Layers.SelectedLayer = layer as IMapFeatureLayer;
                _addShapeFunction = new AddShapeFunction(map as IMap) { Name = "AddShape" };
                _addShapeFunction.Layer = layer;
                if (map.MapFunctions.Contains(_addShapeFunction) == false)
                {
                    map.MapFunctions.Add(_addShapeFunction);
                }
                _addShapeFunction.Activate();
                btnAddPoint.Text = "Finish Adding Points";    
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            map.Refresh();
            Application.DoEvents();
            
        }

        private void FinishAddingPoints()
        {
            RemoveMapFunctions();
            btnAddPoint.Text = "Start Adding Points";

            string fileName = lstboxDataFiles.SelectedItem.ToString();
            IFeatureLayer layer = GetLayer(fileName);
            bool eMode = layer.EditMode;
            layer.EditMode = false;
            layer.Invalidate();
            _appMgr.Map.Refresh();
            Application.DoEvents();
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            if (string.Compare("Start Adding Points", btnAddPoint.Text, false) == 0)
                StartAddPoints();
            else if (string.Compare("Finish Adding Points", btnAddPoint.Text, false) == 0)
                FinishAddingPoints();
                        
            //map.MouseClick +=new MouseEventHandler(Add_Point);           
        }

        private void Add_Point(object sender, MouseEventArgs e)
        {
            try
            {
                Map map = _appMgr.Map as Map;

                if (e.Button == MouseButtons.Left)
                    map.MouseClick -= Add_Point;

                string fileName = lstboxDataFiles.SelectedItem.ToString();
                IFeatureLayer layer = GetLayer(fileName);

                if (layer == null)
                {
                    Logger.Msg("Unable to find layer on map: " + fileName, Microsoft.VisualBasic.MsgBoxStyle.Critical, "Could Not Add Point");
                    btnAddPoint.Enabled = true;
                    return;
                }

                if (layer.DataSet == null)
                {
                    Logger.Msg("Layer does not have an associated dataset:" + fileName, Microsoft.VisualBasic.MsgBoxStyle.Critical, "Could Not Add Point");
                    btnAddPoint.Enabled = true;
                    return;
                }

                map.Layers.SelectedLayer = layer as IMapFeatureLayer;                

                Coordinate lPointMapProjection = map.PixelToProj(new System.Drawing.Point(e.X, e.Y));
                double lLatitude = lPointMapProjection.Y;
                double lLongitude = lPointMapProjection.X;
                double[] pts = { lLongitude, lLatitude };
                Reproject.ReprojectPoints(pts, null, KnownCoordinateSystems.Geographic.World.WGS1984, map.Projection, 0, 1);

                IFeatureSet fs = layer.DataSet;
                Coordinate coord = new Coordinate(pts);
                Feature feature = new Feature(coord);
                fs.AddFeature(feature);

            }
            catch (Exception ex)
            {

            }
        }

        private void Add_Point2(object sender, MouseEventArgs e)
        {

            try
            {

                Map map = _appMgr.Map as Map;
                
                if (e.Button == MouseButtons.Left)
                    map.MouseClick -= Add_Point;

                string fileName = lstboxDataFiles.SelectedItem.ToString();
                IFeatureLayer layer = GetLayer(fileName);

                if (layer == null)
                {
                    Logger.Msg("Unable to find layer on map: " + fileName, Microsoft.VisualBasic.MsgBoxStyle.Critical, "Could Not Add Point");
                    btnAddPoint.Enabled = true;
                    return;
                }

                if (layer.DataSet == null)
                {
                    Logger.Msg("Layer does not have an associated dataset:" + fileName, Microsoft.VisualBasic.MsgBoxStyle.Critical, "Could Not Add Point");
                    btnAddPoint.Enabled = true;
                    return;
                }

                Coordinate lPointMapProjection = map.PixelToProj(new System.Drawing.Point(e.X, e.Y));
                double lLatitude = lPointMapProjection.Y;
                double lLongitude = lPointMapProjection.X;
                double[] pts = { lLongitude, lLatitude };
                Reproject.ReprojectPoints(pts, null, KnownCoordinateSystems.Geographic.World.WGS1984, map.Projection, 0, 1);
                //D4EM.Geo.SpatialOperations.ProjectPoint(lLongitude, lLatitude, g_Map.Projection, DotSpatial.Projections.KnownCoordinateSystems.Geographic.World.WGS1984)

                StringBuilder sbColumns = new StringBuilder();
                for (int i = 0; i < layer.DataSet.DataTable.Columns.Count; i++)
                {
                    if (i != 0)
                        sbColumns.Append(",");

                    sbColumns.Append(layer.DataSet.DataTable.Columns[i].ColumnName);
                }

                //Remove the old layer
                map.Layers.Remove(layer as IMapFeatureLayer);
                if (layer.DataSet != null)
                    layer.DataSet.Close();

                //try writing file            
                atcTableDBF lDBF = new atcTableDBF();
                lDBF.OpenFile(Path.ChangeExtension(fileName, ".dbf"));
                int lFieldIndex = 0;
                string lNewStr = "";
                lNewStr = sbColumns.ToString();
                lNewStr = lNewStr + Microsoft.VisualBasic.Constants.vbCrLf;
                for (int lRecordIndex = 1; lRecordIndex <= lDBF.NumRecords; lRecordIndex++)
                {
                    lDBF.CurrentRecord = lRecordIndex;
                    for (lFieldIndex = 1; lFieldIndex <= lDBF.NumFields; lFieldIndex++)
                    {
                        lNewStr = lNewStr + lDBF.get_Value(lFieldIndex);
                        if (lFieldIndex < lDBF.NumFields)
                        {
                            lNewStr = lNewStr + ",";
                        }
                    }
                    lNewStr = lNewStr + Microsoft.VisualBasic.Constants.vbCrLf;
                }
                lNewStr = lNewStr + string.Format("{0:0.00000000}", lLatitude) + "," + string.Format("{0:0.00000000}", lLongitude) + ",0,0,0,0,0,0,0";
                atcUtility.modFile.SaveFileString(fileName, lNewStr);

                //and then re-read it
                try
                {
                    atcTableDelimited pAnimalsCSV = new atcTableDelimited();
                    pAnimalsCSV.Delimiter = ',';
                    if (pAnimalsCSV.OpenFile(fileName))
                    {
                        ImportLocalData impLocData = new ImportLocalData(_appMgr);
                        string lShapeFileName = atcUtility.modFile.GetTemporaryFileName(Path.GetFileNameWithoutExtension(fileName), ".shp");
                        lShapeFileName = impLocData.CreateShapefileFromTable(lShapeFileName, pAnimalsCSV, 1, 2, map.Projection);
                        if (File.Exists(lShapeFileName))
                        {
                            fileName = lShapeFileName;
                            map.Layers.Add(lShapeFileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.MsgCustomOwned(ex.Message, "Error opening Animals", this, "Ok");
                }

                map.Layers.ResumeEvents();
                map.Refresh();

            }
            catch (Exception ex)
            {

            }

            btnAddPoint.Enabled = false;

        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            RemoveSelectedPoints();
        }

        private void RemoveSelectedPoints()
        {
            bool lFoundSelectedFarm = false;
            string fileName = lstboxDataFiles.SelectedItem.ToString();
            IFeatureLayer layer = GetLayer(fileName);

            if (layer != null)
            {
                System.Collections.Generic.List<DotSpatial.Data.IFeature> lSelectedFeatures = layer.Selection.ToFeatureList();
                if (lSelectedFeatures.Count > 0)
                {
                    lFoundSelectedFarm = true;
                    string lPlural = "";
                    if (lSelectedFeatures.Count > 1)
                        lPlural = "s";
                    if (Logger.MsgCustomOwned("Remove " + lSelectedFeatures.Count + " point" + lPlural + "?", "Remove", this, "Yes", "No") == "Yes")
                    {
                        layer.RemoveSelectedFeatures();

                        layer.DataSet.ShapeIndices = null; // Reset shape indices
                        layer.DataSet.UpdateExtent();
                        layer.AssignFastDrawnStates();
                        layer.DataSet.InvalidateVertices();

                        layer.DataSet.Save();

                        _appMgr.Map.Refresh();
                    }
                }
            }
            if (!lFoundSelectedFarm)
            {
                Logger.MsgCustomOwned("Select one or more points on the map, then delete them.", "No points Selected", this, "Ok");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RemoveMapFunctions()
        {
            // Todo: restore cursor if necessary.
            if (_addShapeFunction != null && _appMgr.Map != null)
            {
                _addShapeFunction.Deactivate();
                if (_appMgr.Map.MapFunctions.Contains(_addShapeFunction))
                {
                    _appMgr.Map.MapFunctions.Remove(_addShapeFunction);
                }

                _addShapeFunction = null;
            }
            if (_moveVertexFunction != null && _appMgr.Map != null)
            {
                _moveVertexFunction.Deactivate();
                if (_appMgr.Map.MapFunctions.Contains(_moveVertexFunction))
                {
                    _appMgr.Map.MapFunctions.Remove(_moveVertexFunction);
                }

                _moveVertexFunction = null;
            }            
        }

        private void btnCloseFile_Click(object sender, EventArgs e)
        {
            string fileName = lstboxDataFiles.SelectedItem.ToString();
            IFeatureLayer layer = GetLayer(fileName);
            if (layer != null)
                layer.DataSet.Save();

            SetButtonsState(false);
        }

    }
}
