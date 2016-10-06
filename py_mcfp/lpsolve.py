from lpsolve55 import *
 
# full file path to DIMACS file
path = 'simple_MCFP.net'
 
# read DIMACS file and create linear program
lp = lpsolve('read_XLI', 'xli_DIMACS', path)
 
# solve linear program
lpsolve('solve', lp)
 
# get total optimum
optimum = lpsolve('get_objective', lp)
 
print optimum #should be 14
