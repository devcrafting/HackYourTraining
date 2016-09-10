FROM mono:4

WORKDIR /var/app

EXPOSE 80

COPY ./build /var/app
COPY ./node_modules /var/app/node_modules

CMD ["mono", "HackYourTraining.exe", "/var/app/www", "/var/app/node_modules", "80"]
