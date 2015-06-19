using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;


namespace D4EMPlugins
{
    class ReadXMLandWriteLog
    {


        public ReadXMLandWriteLog(string xmlfilename, string logfilename, bool logfileExists, string datatype)
        {
            TextWriter output = new StreamWriter(logfilename, logfileExists);

            
            output.WriteLine();
            output.WriteLine(" - - - - - - - - - - - ");            
            output.WriteLine(datatype);
            output.WriteLine(" - - - - - - - - - - - ");  
            output.WriteLine();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse; 

            using (XmlReader reader = XmlReader.Create(xmlfilename, settings))
            {
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {// Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "MetaID":
                                string MetaID = reader.ReadString();                                                            
                                output.WriteLine("MetaID:  " + MetaID);                               
                                break;
                            case "CreaDate":
                                string CreaDate = reader.ReadString();
                                output.WriteLine("CreaDate:  " + CreaDate);
                                break;
                            case "CreaTime":
                                string CreaTime = reader.ReadString();
                                output.WriteLine("CreaTime:  " + CreaTime);
                                break;
                            case "SyncOnce":
                                string SyncOnce = reader.ReadString();
                                output.WriteLine("SyncOnce:  " + SyncOnce);
                                break;
                            case "SyncDate":
                                string SyncDate = reader.ReadString();
                                output.WriteLine("SyncDate:  " + SyncDate);
                                break;
                            case "SyncTime":
                                string SyncTime = reader.ReadString();
                                output.WriteLine("SyncTime:  " + SyncTime);
                                break;
                            case "ModDate":
                                string ModDate = reader.ReadString();
                                output.WriteLine("ModDate:  " + ModDate);
                                break;
                            case "ModTime":
                                string ModTime = reader.ReadString();
                                output.WriteLine("ModTime:  " + ModTime);
                                break;
                            case "Process":
                                string name = reader["Name"];
                                string toolSource = reader["ToolSource"];
                                string date = reader["Date"];
                                string time = reader["Time"];
                                string process = reader.ReadString();
                                output.WriteLine("Process Name:  " + name);
                                output.WriteLine("  ToolSource:  " + toolSource);
                                output.WriteLine("  Date:  " + date);
                                output.WriteLine("  Time:  " + time);
                                break;
                            case "native":
                                string sync = reader["Sync"];
                                string native = reader.ReadString();
                                output.WriteLine("native:  " + native);
                                break;
                            case "langdata":
                                sync = reader["Sync"];
                                string langdata = reader.ReadString();
                                output.WriteLine("langdata:  " + langdata);
                                break;                           
                            case "purpose":
                                string purpose = reader.ReadString();
                                output.WriteLine("purpose:  " + purpose);
                                break;
                            case "origin":
                                string origin = reader.ReadString();
                                output.WriteLine("origin:  " + origin);
                                break;
                            case "pubdate":
                                string pubdate = reader.ReadString();
                                output.WriteLine("pubdate:  " + pubdate);
                                break;
                            case "title":
                                sync = reader["Sync"];
                                string title = reader.ReadString();
                                output.WriteLine("title:  " + title);
                                break;
                            case "ftname":
                                sync = reader["Sync"];
                                string ftname = reader.ReadString();
                                output.WriteLine("ftname:  " + ftname);
                                break;
                            case "geoform":
                                sync = reader["Sync"];
                                string geoform = reader.ReadString();
                                output.WriteLine("geoform:  " + geoform);
                                break;
                            case "onlink":
                                sync = reader["Sync"];
                                string onlink = reader.ReadString();
                                output.WriteLine("onlink:  " + onlink);
                                break;
                            case "current":                                
                                string current = reader.ReadString();
                                output.WriteLine("current:  " + current);
                                break;
                            case "caldate":                                
                                string caldate = reader.ReadString();
                                output.WriteLine("caldate:  " + caldate);
                                break;
                            case "progress":                                
                                string progress = reader.ReadString();
                                output.WriteLine("progress:  " + progress);
                                break;
                            case "update":
                                string update = reader.ReadString();
                                output.WriteLine("update:  " + update);
                                break;
                            case "westbc":
                                sync = reader["Sync"];
                                string westbc = reader.ReadString();
                                output.WriteLine("westbc:  " + westbc);
                                break;
                            case "eastbc":
                                sync = reader["Sync"];
                                string eastbc = reader.ReadString();
                                output.WriteLine("eastbc:  " + eastbc);
                                break;
                            case "northbc":
                                sync = reader["Sync"];
                                string northbc = reader.ReadString();
                                output.WriteLine("northbc:  " + northbc);
                                break;
                            case "southbc":
                                sync = reader["Sync"];
                                string southbc = reader.ReadString();
                                output.WriteLine("southbc:  " + southbc);
                                break;
                            case "themekt":
                                string themekt = reader.ReadString();
                                output.WriteLine("themekt:  " + themekt);
                                break;
                            case "themekey":
                                string themekey = reader.ReadString();
                                output.WriteLine("themekey:  " + themekey);
                                break;
                            case "accconst":
                                string accconst = reader.ReadString();
                                output.WriteLine("accconst:  " + accconst);
                                break;
                            case "useconst":
                                string useconst = reader.ReadString();                               
                                output.WriteLine("useconst:  " + useconst);                                
                                break;
                                /*
                            case "procstep":
                                string procstep = reader.ReadString();
                                output.WriteLine("procstep:  " + procstep);
                                break;
                                */
                            case "procdesc":
                                string procdesc = reader.ReadString();
                                output.WriteLine();
                                output.WriteLine("procdesc:  " + procdesc);
                                break;
                           
                            case "procdate":
                                string procdate = reader.ReadString();
                                output.WriteLine("procdate:  " + procdate);
                                break;
                            case "proctime":
                                string proctime = reader.ReadString();
                                output.WriteLine("proctime:  " + proctime);
                                break;
                        
                            case "envirDesc":
                                sync = reader["Sync"];
                                string envirDesc = reader.ReadString();
                                output.WriteLine("envirDesc:  " + envirDesc);
                                break;
                            case "languageCode":
                                sync = reader["Sync"];
                                string value = reader["value"];                                
                                string languageCode = reader.ReadString();
                                output.WriteLine("languageCode:  " + value);
                                break;
                            case "resTitle":
                                sync = reader["Sync"];                                
                                string resTitle = reader.ReadString();
                                output.WriteLine("resTitle:  " + resTitle);
                                break;
                            case "PresFormCd":
                                sync = reader["Sync"];                                
                                string PresFormCd = reader.ReadString();
                                output.WriteLine("PresFormCd:  " + PresFormCd);
                                break;
                            case "SpatRepTypCd":
                                sync = reader["Sync"];                                
                                string SpatRepTypCd = reader.ReadString();
                                output.WriteLine("SpatRepTypCd:  " + SpatRepTypCd);
                                break;
                            case "langmeta":
                                sync = reader["Sync"];                                
                                string langmeta = reader.ReadString();
                                output.WriteLine();
                                output.WriteLine("langmeta:  " + langmeta);
                                break;
                            case "metstdn":
                                sync = reader["Sync"];                                
                                string metstdn = reader.ReadString();
                                output.WriteLine("metstdn:  " + metstdn);
                                break;
                            case "metstdv":
                                sync = reader["Sync"];                                
                                string metstdv = reader.ReadString();
                                output.WriteLine("metstdv:  " + metstdv);
                                break;
                            case "mettc":
                                sync = reader["Sync"];                                
                                string mettc = reader.ReadString();
                                output.WriteLine("mettc:  " + mettc);
                                break;
                            case "metprof":
                                sync = reader["Sync"];                                
                                string metprof = reader.ReadString();
                                output.WriteLine("metprof:  " + metprof);
                                break;
                            case "cntper":
                                string cntper = reader.ReadString();
                                output.WriteLine("cntper:  " + cntper);
                                break;
                            case "cntorg":
                                string cntorg = reader.ReadString();
                                output.WriteLine("cntorg:  " + cntorg);
                                break;
                            case "addrtype":
                                string addrtype = reader.ReadString();
                                output.WriteLine("addrtype:  " + addrtype);
                                break;
                            case "city":
                                string city = reader.ReadString();
                                output.WriteLine("city:  " + city);
                                break;
                            case "state":
                                string state = reader.ReadString();
                                output.WriteLine("state:  " + state);
                                break;
                            case "postal":
                                string postal= reader.ReadString();
                                output.WriteLine("postal:  " + postal);
                                break;
                            case "cntvoice":
                                string cntvoice = reader.ReadString();
                                output.WriteLine("cntvoice:  " + cntvoice);
                                break;                     
                            case "mdStanName":
                                sync = reader["Sync"];                                
                                string mdStanName = reader.ReadString();
                                output.WriteLine("mdStanName:  " + mdStanName);
                                break;
                            case "mdStanVer":
                                sync = reader["Sync"];                                
                                string mdStanVer = reader.ReadString();
                                output.WriteLine("mdStanVer:  " + mdStanVer);
                                break;
                            case "CharSetCd":
                                sync = reader["Sync"];    
                                value = reader["value"];                              
                                string CharSetCd = reader.ReadString();
                                output.WriteLine("CharSetCd:  " + value);
                                break;
                            case "ScopeCd":
                                sync = reader["Sync"];    
                                value = reader["value"];                              
                                string ScopeCd = reader.ReadString();
                                output.WriteLine("ScopeCd:  " + value);
                                break;
                            case "mdHrLvName":
                                sync = reader["Sync"];                                
                                string mdHrLvName = reader.ReadString();
                                output.WriteLine("mdHrLvName:  " + mdHrLvName);
                                break;
                            case "resdesc":
                                sync = reader["Sync"];                                
                                string resdesc = reader.ReadString();
                                output.WriteLine("resdesc:  " + resdesc);
                                break;
                            case "transize":
                                sync = reader["Sync"];                                
                                string transize = reader.ReadString();
                                output.WriteLine("transize:  " + transize);
                                break;
                            case "dssize":
                                sync = reader["Sync"];                                
                                string dssize = reader.ReadString();
                                output.WriteLine("dssize:  " + dssize);
                                break;
                            case "orDesc":
                                sync = reader["Sync"];                                
                                string orDesc  = reader.ReadString();
                                output.WriteLine();
                                output.WriteLine("orDesc:  " + orDesc);
                                break;
                            case "linkage":
                                sync = reader["Sync"];                                
                                string linkage  = reader.ReadString();
                                output.WriteLine("linkage:  " + linkage);
                                break;
                            case "protocol":
                                sync = reader["Sync"];                                
                                string protocol  = reader.ReadString();
                                output.WriteLine("protocol:  " + protocol);
                                break;
                            case "transSize":
                                sync = reader["Sync"];                                
                                string transSize   = reader.ReadString();
                                output.WriteLine("transSize:  " + transSize);
                                break;
                            case "formatName":
                                sync = reader["Sync"];                                
                                string formatName  = reader.ReadString();
                                output.WriteLine("formatName:  " + formatName);
                                break;
                            case "direct":
                                sync = reader["Sync"];                                
                                string direct = reader.ReadString();
                                output.WriteLine();
                                output.WriteLine("direct:  " + direct);
                                break;
                            case "esriterm":
                                string Name= reader["Name"];
                                output.WriteLine("esriterm Name:  " + Name);                                
                                break;
                            case "efeatyp":
                                sync = reader["Sync"];                                
                                string efeatyp = reader.ReadString();
                                output.WriteLine("efeatyp:  " + efeatyp);
                                break;
                            case "efeageom":
                                sync = reader["Sync"];                                
                                string efeageom = reader.ReadString();
                                output.WriteLine("efeageom:  " + efeageom);
                                break;
                            case "esritopo":
                                sync = reader["Sync"];                                
                                string esritopo= reader.ReadString();
                                output.WriteLine("esritopo:  " + esritopo);
                                break;
                            case "efeacnt":
                                sync = reader["Sync"];                                
                                string efeacnt = reader.ReadString();
                                output.WriteLine("efeacnt:  " + efeacnt);
                                break;
                            case "spindex":
                                sync = reader["Sync"];                                
                                string spindex  = reader.ReadString();
                                output.WriteLine("spindex:  " + spindex);
                                break;
                            case "linrefer":
                                sync = reader["Sync"];                                
                                string linrefer = reader.ReadString();
                                output.WriteLine("linrefer:  " + linrefer);
                                break;
                            case "sdtsterm":
                                Name= reader["Name"];
                                output.WriteLine("sdtsterm Name:  " + Name);
                                break;
                            case "sdtstype":
                                sync = reader["Sync"];                                
                                string sdtstype = reader.ReadString();
                                output.WriteLine("sdtstype:  " + sdtstype);
                                break;
                            case "ptvctcnt":
                                sync = reader["Sync"];                                
                                string ptvctcnt = reader.ReadString();
                                output.WriteLine("ptvctcnt:  " + ptvctcnt);
                                break;
                            case "geogcsn":
                                sync = reader["Sync"];                                
                                string geogcsn = reader.ReadString();
                                output.WriteLine();
                                output.WriteLine("geogcsn:  " + geogcsn);
                                break;
                            case "geogunit":
                                sync = reader["Sync"];                                
                                string geogunit = reader.ReadString();
                                output.WriteLine("geogunit:  " + geogunit);
                                break;
                            case "latres":
                                sync = reader["Sync"];                                
                                string latres = reader.ReadString();
                                output.WriteLine("latres:  " + latres);
                                break;
                            case "longres":
                                sync = reader["Sync"];                                
                                string longres= reader.ReadString();
                                output.WriteLine("longres:  " + longres);
                                break;
                            case "horizdn":
                                sync = reader["Sync"];                                
                                string horizdn = reader.ReadString();
                                output.WriteLine("horizdn:  " + horizdn);
                                break;
                            case "ellips":
                                sync = reader["Sync"];                                
                                string ellips= reader.ReadString();
                                output.WriteLine("ellips:  " + ellips);
                                break;
                            case "semiaxis":
                                sync = reader["Sync"];                                
                                string semiaxis = reader.ReadString();
                                output.WriteLine("semiaxis:  " + semiaxis);
                                break;
                            case "denflat":
                                sync = reader["Sync"];                                
                                string denflat = reader.ReadString();
                                output.WriteLine("denflat:  " + denflat);
                                break;
                            case "identCode":
                                sync = reader["Sync"];                                
                                string identCode = reader.ReadString();
                                output.WriteLine();
                                output.WriteLine("identCode:  " + identCode);
                                break;
                            case "TopoLevCd":
                                sync = reader["Sync"];           
                                value = reader["value"];  
                                string TopoLevCd = reader.ReadString();
                                output.WriteLine();
                                output.WriteLine("TopoLevCd:  " + value);
                                break;
                            case "GeoObjTypCd":
                                sync = reader["Sync"];           
                                value = reader["value"];  
                                string GeoObjTypCd = reader.ReadString();
                                output.WriteLine("GeoObjTypCd:  " + value);
                                break;
                            case "geoObjCnt":
                                sync = reader["Sync"];       
                                string geoObjCnt = reader.ReadString();
                                output.WriteLine("geoObjCnt:  " + geoObjCnt);
                                break;
                            case "detailed":
                                Name= reader["Name"];
                                output.WriteLine("detailed Name:  " + Name);
                                break;
                            case "enttypl":
                                sync = reader["Sync"];       
                                string enttypl = reader.ReadString();
                                output.WriteLine();
                                output.WriteLine("enttypl:  " + enttypl);
                                break;
                            case "enttypt":
                                sync = reader["Sync"];       
                                string enttypt = reader.ReadString();
                                output.WriteLine("enttypt:  " + enttypt);
                                break;
                            case "enttypc":
                                sync = reader["Sync"];       
                                string enttypc = reader.ReadString();
                                output.WriteLine("enttypc:  " + enttypc);
                                break;
                            case "attrlabl":
                                sync = reader["Sync"];       
                                string attrlabl = reader.ReadString();
                                output.WriteLine();
                                output.WriteLine("attrlabl:  " + attrlabl);
                                break;
                            case "attalias":
                                sync = reader["Sync"];       
                                string attalias = reader.ReadString();
                                output.WriteLine("attalias:  " + attalias);
                                break;
                            case "attrtype":
                                sync = reader["Sync"];       
                                string attrtype = reader.ReadString();
                                output.WriteLine("attrtype:  " + attrtype);
                                break;
                            case "attwidth":
                                sync = reader["Sync"];       
                                string attwidth = reader.ReadString();
                                output.WriteLine("attwidth:  " + attwidth);
                                break;
                            case "atprecis":
                                sync = reader["Sync"];       
                                string atprecis = reader.ReadString();
                                output.WriteLine("atprecis:  " + atprecis);
                                break;
                            case "attscale":
                                sync = reader["Sync"];       
                                string attscale = reader.ReadString();
                                output.WriteLine("attscale:  " + attscale);
                                break;
                            case "attrdef":
                                sync = reader["Sync"];       
                                string attrdef  = reader.ReadString();
                                output.WriteLine("attrdef:  " + attrdef);
                                break;
                            case "attrdefs":
                                sync = reader["Sync"];       
                                string attrdefs  = reader.ReadString();
                                output.WriteLine("attrdefs:  " + attrdefs);
                                break;
                            case "udom":
                                sync = reader["Sync"];       
                                string udom = reader.ReadString();
                                output.WriteLine("udom:  " + udom);
                                break;
                            case "mdDateSt":
                                sync = reader["Sync"];
                                string mdDateSt = reader.ReadString();
                                output.WriteLine("mdDateSt:  " + mdDateSt);
                                break;                            
                            case "begdate":
                                string begdate = reader.ReadString();
                                output.WriteLine("begdate:  " + begdate);
                                break;
                            case "enddate":
                                string enddate = reader.ReadString();
                                output.WriteLine("enddate:  " + enddate);
                                break; 
                            case "PAMRasterBand":
                                string band = reader["band"];
                                output.WriteLine("PAMRasterBand:  " + band);
                                break; 
                            case "MDI":
                                string key = reader["key"];
                                string MDI = reader.ReadString();
                                output.WriteLine("MDI   " + key + "   " + MDI);
                                break;         
                            case "metadata":
                                string metadata = reader.ReadString();       
                                output.WriteLine("metadata:  " + metadata);
                                break;
                            case "idinfo":
                                string idinfo = reader.ReadString();
                                output.WriteLine("idinfo:  " + idinfo);
                                break;  
                            case "citation":
                                string citation = reader.ReadString();                               
                                output.WriteLine("citation:  " + citation);                                
                                break;
                            case "citeinfo":
                                string citeinfo = reader.ReadString();
                                output.WriteLine("citeinfo:  " + citeinfo);
                                break; 
                            case "abstract":
                                string _abstract = reader.ReadString();     
                                output.WriteLine("abstract:  " + _abstract);
                                break; 
                            case "timeperd":
                                string timeperd = reader.ReadString();                                
                                output.WriteLine("timeperd:  " + timeperd);                                
                                break;
                            case "timeinfo":
                                string timeinfo = reader.ReadString();
                                output.WriteLine("timeinfo:  " + timeinfo);
                                break;    
                         /*   case "rngdates":
                                string rngdates = reader.ReadString();                                
                                output.WriteLine("rngdates:  " + rngdates);                               
                                break; */
                            case "status":
                                string status = reader.ReadString();                                
                                output.WriteLine("status:  " + status);                               
                                break;    
                            case "spdom":
                                string spdom = reader.ReadString();                               
                                output.WriteLine("spdom:  " + spdom);                               
                                break;     
                            case "keywords":
                                string keywords = reader.ReadString();                               
                                output.WriteLine("keywords:  " + keywords);                               
                                break;   
                         /*   
                            case "dataqual":
                                string dataqual = reader.ReadString();
                                string logic = reader["logic"];
                                string complete = reader["complete"];                  
                                output.WriteLine("dataqual:  " + dataqual);
                                output.WriteLine("logic:  " + logic);
                                output.WriteLine("complete:  " + complete);
                                break;   
                          */
                                
                            case "lineage":
                                string lineage = reader.ReadString();                             
                                output.WriteLine("lineage:  " + lineage);   
                                break;   
                        /*     
                            case "metainfo":
                                string metainfo = reader.ReadString();                            
                                output.WriteLine("metainfo:  " + metainfo);                               
                                break;
                         */
                            case "metd":
                                string metd = reader.ReadString();
                                output.WriteLine();
                                output.WriteLine("metd:  " + metd);
                                break;   
                            case "metc":
                                string metc = reader.ReadString();
                                output.WriteLine("metc:  " + metc);                                
                                break;   
                            case "cntinfo":
                                string cntinfo = reader.ReadString();                                
                                output.WriteLine("cntinfo:  " + cntinfo);                                
                                break;   
                            case "cntperp":
                                string cntperp = reader.ReadString();                                
                                output.WriteLine("cntperp:  " + cntperp);                                
                                break;                                   
                           /* case "cntaddr":
                                string cntaddr = reader.ReadString();                                
                                output.WriteLine("cntaddr:  " + cntaddr);                                
                                break;       */    
                            case "supplinf":
                                string supplinf = reader.ReadString();                                
                                output.WriteLine("supplinf:  " + supplinf);                                
                                break;
                            case "placekt":
                                string placekt = reader.ReadString();
                                output.WriteLine("placekt:  " + placekt);
                                break;
                            case "placekey":
                                string placekey = reader.ReadString();
                                output.WriteLine("placekey:  " + placekey);
                                break;
                            case "cntpos":
                                string cntpos = reader.ReadString();
                                output.WriteLine("cntpos:  " + cntpos);
                                break;
                            case "address":
                                string address = reader.ReadString();
                                output.WriteLine("address:  " + address);
                                break;
                            case "country":
                                string country = reader.ReadString();
                                output.WriteLine("country:  " + country);
                                break;
                            case "cnttdd":
                                string cnttdd = reader.ReadString();
                                output.WriteLine("cnttdd:  " + cnttdd);
                                break;
                            case "cntfax":
                                string cntfax = reader.ReadString();
                                output.WriteLine("cntfax:  " + cntfax);
                                break;  
                            case "cntemail":
                                string cntemail = reader.ReadString();
                                output.WriteLine("cntemail:  " + cntemail);
                                break;  
                            case "hours":
                                string hours = reader.ReadString();
                                output.WriteLine("hours:  " + hours);
                                break;  
                            case "cntinst":
                                string cntinst = reader.ReadString();
                                output.WriteLine("cntinst:  " + cntinst);
                                break;  
                            case "datacred":
                                string datacred = reader.ReadString();
                                output.WriteLine("datacred:  " + datacred);
                                break;  
                            case "secsys":
                                string secsys = reader.ReadString();
                                output.WriteLine("secsys:  " + secsys);
                                break;  
                            case "secclass":
                                string secclass = reader.ReadString();
                                output.WriteLine("secclass:  " + secclass);
                                break;  
                            case "sechandl":
                                string sechandl= reader.ReadString();
                                output.WriteLine("sechandl:  " + sechandl);
                                break;  
                            case "attraccr":
                                string attraccr= reader.ReadString();
                                output.WriteLine("attraccr:  " + attraccr);
                                break;  
                            case "attraccv":
                                string attraccv= reader.ReadString();
                                output.WriteLine("attraccv:  " + attraccv);
                                break;  
                            case "attracce":
                                string attracce= reader.ReadString();
                                output.WriteLine("attracce:  " + attracce);
                                break;  
                            case "logic":
                                string logic= reader.ReadString();
                                output.WriteLine("logic:  " + logic);
                                break;  
                            case "complete":
                                string complete= reader.ReadString();
                                output.WriteLine("complete:  " + complete);
                                break;  
                            case "horizpar":
                                string horizpar= reader.ReadString();
                                output.WriteLine("horizpar:  " + horizpar);
                                break;  
                            case "vertaccr":
                                string vertaccr= reader.ReadString();
                                output.WriteLine("vertaccr:  " + vertaccr);
                                break;  
                            case "srcused":
                                string srcused= reader.ReadString();
                                output.WriteLine("srcused:  " + srcused);
                                break;  
                            case "srcprod":
                                string srcprod= reader.ReadString();
                                output.WriteLine("srcprod:  " + srcprod);
                                break;              
                            case "rasttype":
                                string rasttype= reader.ReadString();
                                output.WriteLine("rasttype:  " + rasttype);
                                break;  
                            case "rowcount":
                                string rowcount= reader.ReadString();
                                output.WriteLine("rowcount:  " + rowcount);
                                break;  
                            case "colcount":
                                string colcount= reader.ReadString();
                                output.WriteLine("colcount:  " + colcount);
                                break;  
                            case "vrtcount":
                                string vrtcount= reader.ReadString();
                                output.WriteLine("vrtcount:  " + vrtcount);
                                break;  
                            case "mapprojn":
                                string mapprojn= reader.ReadString();
                                output.WriteLine("mapprojn:  " + mapprojn);
                                break;  
                            case "stdparll":
                                string stdparll= reader.ReadString();
                                output.WriteLine("stdparll:  " + stdparll);
                                break;  
                            case "longcm":
                                string longcm= reader.ReadString();
                                output.WriteLine("longcm:  " + longcm);
                                break;  
                            case "latprjo":
                                string latprjo= reader.ReadString();
                                output.WriteLine("latprjo:  " + latprjo);
                                break;  
                            case "feast":
                                string feast= reader.ReadString();
                                output.WriteLine("feast:  " + feast);
                                break;  
                            case "fnorth":
                                string fnorth= reader.ReadString();
                                output.WriteLine("fnorth:  " + fnorth);
                                break;  
                            case "plance":
                                string plance= reader.ReadString();
                                output.WriteLine("plance:  " + plance);
                                break;  
                            case "coordrep":
                                string coordrep= reader.ReadString();
                                output.WriteLine("coordrep:  " + coordrep);
                                break;  
                            case "absres":
                                string absres= reader.ReadString();
                                output.WriteLine("absres:  " + absres);
                                break;  
                            case "ordres":
                                string ordres= reader.ReadString();
                                output.WriteLine("ordres:  " + ordres);
                                break;  
                            case "rdommin":
                                string rdommin= reader.ReadString();
                                output.WriteLine("rdommin:  " + rdommin);
                                break;  
                            case "rdommax":
                                string rdommax= reader.ReadString();
                                output.WriteLine("rdommax:  " + rdommax);
                                break;  
                           case "attrunit":
                                string attrunit = reader.ReadString();
                                output.WriteLine("attrunit:  " + attrunit);
                                break;  
                            case "attrmres":
                                string attrmres= reader.ReadString();
                                output.WriteLine("attrmres:  " + attrmres);
                                break;  
                            case "eaover":
                                string eaover= reader.ReadString();
                                output.WriteLine("eaover:  " + eaover);
                                break;  
                            case "eadetcit":
                                string eadetcit= reader.ReadString();
                                output.WriteLine("eadetcit:  " + eadetcit);
                                break;  
                            case "distliab":
                                string distliab = reader.ReadString();
                                output.WriteLine("distliab:  " + distliab);
                                break;  
                             case "formname":
                                string formname = reader.ReadString();
                                output.WriteLine("formname:  " + formname);
                                break;  
                            case "formvern":
                                string formvern = reader.ReadString();
                                output.WriteLine("formvern:  " + formvern);
                                break;  
                            case "formspec":
                                string formspec = reader.ReadString();
                                output.WriteLine("formspec:  " + formspec);
                                break;     
    

                        }
                    }
                }
            }
            output.Close();
        }
    }
}

    

