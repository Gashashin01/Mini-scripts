import os
from zipfile import ZipFile
from ftplib import FTP_TLS

os.system("ipconfig/all>D:\mac.txt")
with ZipFile("mac archive.zip","w") as newzip:
    newzip.write("D:\mac.txt")

#Write your servername, username and password in quotes

server = ''
usern = ''
passwd = ''

file = open("mac archive.zip","rb")

ftp = FTP_TLS()
ftp.set_debuglevel(2)
ftp.connect(server,21)
ftp.sendcmd("USER "+str(usern))
ftp.sendcmd("PASS "+str(passwd))
ftp.storbinary("STOR "+"mac archive.zip",file)
ftp.close

