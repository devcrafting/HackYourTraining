FROM mono:4

WORKDIR /var/app

EXPOSE 80

COPY ./build /var/app

CMD ["mono", "HackYourTraining.exe", "/var/app/www", "80"]
