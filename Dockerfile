FROM mono:4

WORKDIR /var/app

EXPOSE 4000

COPY ./build /var/app
COPY ./www /var/app/www

CMD ["mono", "HackYourTraining.exe", "/var/app/www", "4000"]
