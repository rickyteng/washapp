import os

f = open('list_parent_dir.txt', 'w')
for i in os.listdir('..'):
    f.write('{0}\n'.format(i))
f.close()
