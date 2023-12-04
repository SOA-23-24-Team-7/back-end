-- ******************************** USERS ********************************

INSERT INTO stakeholders."Users"(
	"Id", "Username", "Password", "Role", "IsActive","ProfilePicture")
	VALUES (-168, 'dragan', 'dragan', 2, 'True', 'https://www.espreso.co.rs/data/images/2023/01/17/12/1317487_sin-dragan_share.jpg'),
	(-169, 'sima', 'sima', 2, 'True', 'https://media.discordapp.net/attachments/605735235476520972/1175913776986460180/1700430082262.jpg'),
	(-170, 'somi', 'somi', 1, 'True', 'https://cdn.discordapp.com/attachments/1165638888082124852/1173981466925994095/image.png'),
	(-171, 'brmbrm', 'lozinka', 2, 'True', 'https://cdn.discordapp.com/attachments/1165638888082124852/1174057297807409222/1699987465617.jpg?ex=65663510&is=6553c010&hm=9e7d8441134deebb3d03cd901b291eb2eabccb325c856ff7ead67a8e508d74d5&'),
	(-172, 'buki(k****)', 'lozinka', 2, 'True', 'https://cdn.discordapp.com/attachments/1165638888082124852/1174057297484451890/1699987465609.jpg?ex=65663510&is=6553c010&hm=76cd42499b60ee0eeb1d21c1d7268158c9f6369dcbf1f5152e6fa8d15fb4359e&');
INSERT INTO stakeholders."Users"("Id", "Username", "Password", "Role", "ProfilePicture", "IsActive")
VALUES
    (-1, 'dop', 'dop123', 0, '', true),
    (-2, 'author', 'author', 1, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw', true),
    (-3, 'tourist', 'tourist', 2, 'https://imgs.search.brave.com/_8gIhJxRAq9aqREpTnh_wGcNfv4JwgEssYFmzKDWow8/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5nZXR0eWltYWdl/cy5jb20vaWQvMTMw/MTAwMzg3NC9waG90/by95b3VuZy13b21h/bi1hcnJpdmluZy1h/dC1hLXRyb3BpY2Fs/LXJlc29ydC1mb3It/aGVyLXZhY2F0aW9u/LmpwZz9zPTYxMng2/MTImdz0wJms9MjAm/Yz1BT3N1eWltalZm/QkducTZZeVVhOVZ0/aXRQWE9rdWJOMDFq/TFp0R19lb1ZjPQ', true);

-- ******************************** PEOPLE ********************************

INSERT INTO stakeholders."People" ("Id", "UserId", "Name", "Surname", "Email", "Bio", "Motto") VALUES (-169, -169, 'Filip', 'Simic', 'fsimic346@gmail.com', NULL, 'Brt, guglaj o.0'),
(-170, -170, 'Milos', 'Milutinovic', 'somi@gmail.com', NULL, NULL),
(-168, -168, 'Dragoslav', 'Maslac', 'gagi@gmail.com', NULL, NULL),
(-171, -171, 'Vlada', 'Devic', 'brmbrm@gmail.com', NULL, NULL),
(-172, -172, 'Mihajlo', 'Bukarica', 'buki@gmail.com', NULL, NULL),
(-1, -2, 'John', 'Johnson', 'author@gmail.com', 'I love making tours.', 'Never give up.'),
(-2, -3, 'Charles', 'Smith', 'tourist@gmail.com', 'I love tours.', 'Stay strong.');

-- ******************************** FOLLOWERS ******************************

INSERT INTO stakeholders."Followers" ("Id", "UserId", "FollowedById") VALUES (-1, -170, -169),
(-2, -172, -169),
(-3, -171, -169),
(-4, -168, -169),
(-5, -169, -168),
(-6, -169, -170);

-- ******************************** BLOGS ********************************

INSERT INTO blog."Blogs" ("Id", "Title", "Description", "Date", "Status", "AuthorId", "Votes") VALUES (-11, 'Off-the-Beaten-Path Travel', 'Discover charming villages and secret natural havens off the tourist path. Immerse yourself in the heart of local cultures and explore hidden treasures. Pack your bags for an authentic travel experience!

![Village Retreat](https://live.staticflickr.com/65535/53244354274_f096dbce42.jpg)

Join us on the road less traveled!', '2023-11-14 00:00:00+01', 3, -170, '[{"UserId": -168, "VoteType": 1}, {"UserId": -170, "VoteType": 1}]'),
(-12, 'Historical Wonders: Time Travel Edition', 'Step into history with ancient ruins and medieval castles. Explore the mysteries of past civilizations and witness the grandeur of medieval architecture. Join us on a captivating journey through time!

![Historical Landmarks](https://live.staticflickr.com/65535/53280758885_34afd10b3a.jpg)

Walk the corridors of history with us!
', '2023-11-14 00:00:00+01', 1, -169, '[{"UserId": -168, "VoteType": 0}, {"UserId": -170, "VoteType": 0}]'),
(-13, 'Urban Wanderlust: City Exploration', 'Delve into the heartbeat of vibrant cities! Explore bustling markets, iconic landmarks, and hidden gems within the urban landscape. Immerse yourself in the culture and energy of city life.

![City Exploration](https://live.staticflickr.com/65535/53308142182_84983c1cc3.jpg)

Experience the pulse of the city with us!', '2023-11-14 00:00:00+01', 0, -170, '[]'),
(-14, 'Team building grupa 1 (produžeci)', 'Nastavak kod Dragana na svirku i drinkić ![Team building](https://cdn.discordapp.com/attachments/1165638888082124852/1174054126708064327/IMG-20231113-WA0004.jpg?ex=6566321c&is=6553bd1c&hm=be7fc6baf1721ed5fcc861db7fb1692075556efcf6f95d12210aadbacda3cb8e&)', '2023-11-14 00:00:00+01', 1, -170, '[]'),
(-15, 'Team building grupa 1', 'Divno veče u ambijentu još divnijih ljudi huh ![Team building](https://cdn.discordapp.com/attachments/1165638888082124852/1174053744795734026/IMG-20231113-WA0006.jpg?ex=656631c1&is=6553bcc1&hm=ca8f361cba4201ae655e7465673fbb8b214aa835c5903a8b41ec3b2c299f2233&)', '2023-11-14 00:00:00+01', 1, -170, '[]');

INSERT INTO blog."Comments" ("Id", "AuthorId", "BlogId", "CreatedAt", "UpdatedAt", "Text") VALUES (-1, -168, -11, '2023-11-14 13:49:10.108651+01', NULL, 'dobar blog!'),
(-2, -168, -11, '2023-11-14 13:49:23.737314+01', NULL, 'opet sam procitao, i jos je bolji...'),
(-4, -170, -14, '2023-11-14 13:49:10.108651+01', NULL, 'dobar blog!'),
(-5, -171, -14, '2023-11-14 13:58:16.459035+01', NULL, 'mene na pivo ne zovete, a?'),
(-6, -172, -14, '2023-11-14 13:49:10.108651+01', NULL, 'ni ja nisam pozvan 😔'),
(-3, -169, -14, '2023-11-14 13:58:16.459035+01', NULL, 'drugi put');


-- **************************** TOURS ************************************
INSERT INTO tours."Tours"(
	"Id", "AuthorId", "Name", "Description", "Difficulty", "Tags", "Status", "Price", "IsDeleted", "Distance", "PublishDate", "ArchiveDate", "Durations")
	VALUES
		(-1, -2, 'Novo naselje', 'Tura po Novom naselju', 2, '{tag1,tag2}', 1, 100, FALSE, 2.0, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 0, "TransportType": 1}]'),
		(-2, -2, 'Novo naselje 2', 'Tura po Novom naselju', 2, '{tag1,tag2}', 1, 100, FALSE, 2.0, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 0, "TransportType": 1}]'),
		(-3, -2, 'Novo naselje 3', 'Tura po Novom naselju', 2, '{tag1,tag2}', 1, 100, FALSE, 2.0, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 0, "TransportType": 1}]'),
		(-4, -2, 'Novo naselje 4', 'Tura po Novom naselju', 2, '{tag1,tag2}', 1, 100, FALSE, 2.0, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 0, "TransportType": 1}]'),
		(-5, -2, 'Novo naselje 5', 'Tura po Novom naselju', 2, '{tag1,tag2}', 1, 100, FALSE, 2.0, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 0, "TransportType": 1}]'),
		(-6, -2, 'Novo naselje 6', 'Tura po Novom naselju', 2, '{tag1,tag2}', 1, 100, FALSE, 2.0, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 0, "TransportType": 1}]'),
		(-7, -2, 'Novo naselje 7', 'Tura po Novom naselju', 2, '{tag1,tag2}', 1, 100, FALSE, 2.0, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 0, "TransportType": 1}]'),
		(-8, -2, 'Novo naselje 8', 'Tura po Novom naselju', 2, '{tag1,tag2}', 1, 100, FALSE, 2.0, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 0, "TransportType": 1}]'),
		(-9, -2, 'Novo naselje 9', 'Tura po Novom naselju', 2, '{tag1,tag2}', 1, 100, FALSE, 2.0, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 0, "TransportType": 1}]'),
		(-10, -2, 'Novo naselje 10', 'Tura po Novom naselju', 2, '{tag1,tag2}', 1, 100, FALSE, 2.0, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 0, "TransportType": 1}]');