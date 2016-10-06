# -*- coding: latin-1 -*-
#  Helper for GRASS GIS Python Scripting
#  Johannes Sommer, 18.01.2012
 
class utility():
    import core as grass
    ''' Utility-Class for GRASS GIS Python Scripting.
        Copyright by Johannes Sommer 2012.
 
        - doAddColumn
        - doCalcColumnValue
        - doRenameColumn
        - doIntersection
        - doDifference
        - getMapsets
        - getVectorLayers
        - getRasterLayers
        - getVectorLayersFromMapset
        - getRasterLayersFromMapset
        - getColumnList
        - doFilterLayerList 
 
        Usage:
        import grass.utils as utils
        gUtil = util.utility()
        gUtil.dodifference('forest', lakes')
        '''
 
    def doAddColumn(self, _lyrA, _colName, _colType):
        ''' Adds a column to the input vector layer with the given type.
 
            GRASS datatypes depend on underlying database, but all
            support:
            - varchar (length)
            - double precision
            - integer
            - date
 
            Usage:
            doAddColumn('lakes', 'width', 'double precision')   '''
        retVal = grass.run_command('v.db.addcol', map=_lyrA,
                                   columns='%s %s' % (_colName, _colType) )
        return retVal
 
    def doCalcColumnValue(self, _lyrA, _colName, _newVal):
        ''' Updates a column of the input vector layer with the passed value.
            @_newVal can be a certain value or a calculation of existing columns.
 
            Usage:
            doCalcColumnValue('lakes', 'width', 'length/2')     '''
        retVal = grass.run_command('v.db.update', map=_lyrA,
                                   column=_colName, value=_newVal)
        return retVal
 
    def doRenameColumn(self, _lyrA, _colNameA, _colNameB):
        ''' Renames a column of the input vector layer.
 
            Usage:
            doRenameColumn('lakes', 'width', 'width_old')       '''
        retVal = grass.run_command('v.db.renamecol', map=_lyrA,
                                   column='%s,%s' %(_colNameA,_colNameB))
        return retVal
 
    def doIntersection(self, _lyrA, _lyrB, OVERWRITE_OUTPUT==False):
        ''' Performs a geometric intersection ('AND'-Operator) of two vector layers
            and writes the output to a layer called 'layerNameA_X_layerNameB'.
 
            @OVERWRITE_OUTPUT can be set to 'Y' or 'N'
            Usage:
            doIntersection('lakes', 'forest')   '''
        outputLyr = _lyrA.split('@')[0] + '_X_' + _lyrB.split('@')[0]
 
        optArg = ''
        if OVERWRITE_OUTPUT == True:
            optArg = '--overwrite'
        retVal = grass.run_command('v.overlay %s' % optArg, ainput=_lyrA,
                                   binput=_lyrB, output=outputLyr,
                                   operator='and')
        return retVal
 
    def doDifference(self, _lyrA, _lyrB, OVERWRITE_OUTPUT==False):
        ''' Performs a geometric difference ('NOT'-Operator) of two vector layers
            and writes the output to a layer called 'layerNameA_DIFF_layerNameB'.
 
            @OVERWRITE_OUTPUT can be set to 'Y' or 'N'
            Usage:
            doDifference('lakes', 'forest') '''
        outputLyr = _lyrA.split('@')[0] + '_DIFF_' + _lyrB.split('@')[0]
 
        optArg = ''
        if OVERWRITE_OUTPUT == True':
            optArg = '--overwrite'
        retVal = grass.run_command('v.overlay %s' % optArg, ainput=_lyrA,
                                   binput=_lyrB, output=outputLyr,
                                   operator='not')
        return retVal
 
    def getMapsets(self):
        ''' Returns all GRASS mapsets in current GRASS location as list.'''
        return grass.mapsets()
 
    def getVectorLayers(self):
        ''' Returns all vector layers in current GRASS location as list.'''
        return grass.list_strings('vect')
 
    def getRasterLayers(self):
        ''' Returns all raster layers in current GRASS location as list.'''
        return grass.list_strings('rast')
 
    def getVectorLayersFromMapset(self, _mapset):
        ''' Returns all vector layers in given mapset as list.'''
        vLyrList = getVectorLayers()
        # Filter List by MAPSET
        vLyrListFiltered = []
        for item in vLyrList:
            if _mapset in item:
                vLyrListFiltered.append(item)
        return vLyrListFiltered
 
    def getRasterLayersFromMapset(self, _mapset):
        ''' Returns all raster layers in given mapset as list.'''
        rLyrList = getRasterLayers()
        # Filter List by MAPSET
        rLyrListFiltered = []
        for item in rLyrList:
            if _mapset in item:
                rLyrListFiltered.append(item)
        return rLyrListFiltered
 
    def getColumnList(self, _vLyr):
        ''' Returns all column names of a vector layer as list.'''
        # Import vector wrapper from %GISBASE%\etc\python\script
        import vector as vector
        out_colList = []
        out_colList = vector.vector_columns(_vLyr, getDict = False)
        return out_colList
 
    def doFilterLayerList(self, _lyrList, _lyrkey):
        ''' Filters input layer list per _lyrkey and returns filtered list.'''
        out_lyrList = []
        for item in _lyrList:
            if _lyrkey in item:
                out_lyrList.append(item)
        return out_lyrList
