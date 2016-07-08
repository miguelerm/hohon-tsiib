# Hohon Ts’íib

Firmar en maya yucateco se escribe **hohon ts’íib**.


## Pasos para generar el certificado autofirmado
Pasos tomados de https://devcenter.heroku.com/articles/ssl-certificate-self

    openssl genrsa -des3 -passout pass:FoevaGWv6TnR3gC0Kc5o -out server.pass.key 2048
	openssl rsa -passin pass:FoevaGWv6TnR3gC0Kc5o -in server.pass.key -out server.key
	openssl req -new -key server.key -out server.csr
	openssl x509 -req -sha256 -days 1024 -in server.csr -signkey server.key -out server.crt
	rm server.pass.key
	rm server.csr


