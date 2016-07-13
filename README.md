# HackYourTraining

Trainings are a good way to raise the bar. With training we could reach people outside of the community.
But trainings are expensive. How can we disrupt classical training with the help of the community?

What about an open source platform to bring together trainers and trainees, removing intermediaries.

We could build pull based training instead of classic push based training:
- Anybody can register on the platform using Twitter
- Users could create a "request for training" with:
	* a topic
	* a city
	* a speaker
	* optionally a time window
- Other users can see the request and register as "interested by this request for training"
- As soon as we reach a certain threshold, a tweet is send to the trainer
- If the trainer follows the link from Twitter, he/she sees the "request for training" information, and he/she can provide:
	* some precise date he/she's available
	* the price for the training
- Then, registered users receive an update and can pay if they still want to participate
- In the meantime, a sponsorship request is sent to pre-identified local sponsors to provide:
	* food
	* rooms
	* in exchange they receive free tickets for the training.

It's really just a draft, feel free to improve the idea and share with us on slack:
https://softwarecraftsmanship.slack.com on the channel hackyourtraining

## Participate to development

You can develop under Linux/OSX/Windows equally, just run the right `restore.and.build.*` script according your platform.
It restores dependencies through Paket and run Fake script to start Suave.io web server (in dev mode, watching changed files).

After having run `restore.and.build.*` script once, you can just run `fake.*`script with an optional target (`build` by default, but also `watch` or `run`).