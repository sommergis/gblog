# -*- coding: latin-1 -*-

# Liest die LayerIDs eines ArcGIS REST Services
# ein und gibt diese aus.
# Johannes Sommer, 12.01.2011

try:
    import simplejson as json
except ImportError:
    print 'Benoetigtes Python-Modul fuer das Skript ist:\n'+\
          ' - Simplejson\n'

# Standard Python Module
import urllib2
URL = "http://server.arcgisonline.com/ArcGIS/rest/services/Demographics/"

def getLayerInfoFromREST(url, _serviceName):
    ''' Liest die Layer-IDs des ArcGIS REST Dienstes aus
        der angegebenen URL und liefert eine Liste mit den
        Layer-IDs (Strings) und den Layernamen zurueck.

        Benoetigtes Format der URL:
        http://<host>/ArcGIS/rest/services/<folder>/'''

    serviceName = _serviceName
    url = url + serviceName + '/MapServer/?f=pjson'

    response = urllib2.urlopen(url).read()
    jsonObj = json.loads(response)

    resultDict = {}

    for key in jsonObj.iterkeys():
        # layers entspricht dem Data Frame Namen des zugrunde-
        # liegenden ArcMap-Dokument und muss evtl. angepasst
        # werden
        if key == 'layers':
            root = jsonObj.get(key)
            for layerInfo in root:

                nameString = layerInfo.get('name')
                id = int(layerInfo.get('id'))
                parentId = int(layerInfo.get('parentLayerId'))

                # Nimm nur die Hauptebenen (ohne Subelemente)
                #if parentId == -1:
                    ## Konvertierung von UTF8 to Latin-1
                    #utfString = unicode(nameString)
                    #latinString = utfString.encode('latin-1')

                # Nimm alle Ebenen
                # Konvertierung von UTF8 to Latin-1
                utfString = unicode(nameString)
                latinString = utfString.encode('latin-1')

                resultDict[id] = latinString

    return resultDict

# Funktionsaufruf
resultDict = getLayerInfoFromREST(URL, "USA_1990-2000_Population_Change")

for item in resultDict.iteritems():
    print item
