-- ******************************** USERS ********************************

INSERT INTO stakeholders."Users"(
	"Id", "Username", "Password", "Role", "IsActive","ProfilePicture")
	VALUES (-168, 'dragan', 'dragan', 2, 'True', 'https://www.espreso.co.rs/data/images/2023/01/17/12/1317487_sin-dragan_share.jpg'),
	(-169, 'sima', 'sima', 2, 'True', 'https://media.discordapp.net/attachments/605735235476520972/1175913776986460180/1700430082262.jpg'),
	(-170, 'somi', 'somi', 1, 'True', 'https://cdn.discordapp.com/attachments/1165638888082124852/1173981466925994095/image.png'),
	(-171, 'brmbrm', 'lozinka', 2, 'True', 'https://cdn.discordapp.com/attachments/1165638888082124852/1174057297807409222/1699987465617.jpg?ex=65663510&is=6553c010&hm=9e7d8441134deebb3d03cd901b291eb2eabccb325c856ff7ead67a8e508d74d5&'),
	(-172, 'buki(k****)', 'lozinka', 2, 'True', 'https://cdn.discordapp.com/attachments/1165638888082124852/1174057297484451890/1699987465609.jpg?ex=65663510&is=6553c010&hm=76cd42499b60ee0eeb1d21c1d7268158c9f6369dcbf1f5152e6fa8d15fb4359e&'),
    (-1, 'dop', 'dop123', 0, true, ''),
    (-2, 'author', 'author', 1, true, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw'),
    (-3, 'tourist', 'tourist', 2, true, 'https://imgs.search.brave.com/_8gIhJxRAq9aqREpTnh_wGcNfv4JwgEssYFmzKDWow8/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5nZXR0eWltYWdl/cy5jb20vaWQvMTMw/MTAwMzg3NC9waG90/by95b3VuZy13b21h/bi1hcnJpdmluZy1h/dC1hLXRyb3BpY2Fs/LXJlc29ydC1mb3It/aGVyLXZhY2F0aW9u/LmpwZz9zPTYxMng2/MTImdz0wJms9MjAm/Yz1BT3N1eWltalZm/QkducTZZeVVhOVZ0/aXRQWE9rdWJOMDFq/TFp0R19lb1ZjPQ');
    (-4, 'tourist1', 'tourist', 2, true, 'https://imgs.search.brave.com/_8gIhJxRAq9aqREpTnh_wGcNfv4JwgEssYFmzKDWow8/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5nZXR0eWltYWdl/cy5jb20vaWQvMTMw/MTAwMzg3NC9waG90/by95b3VuZy13b21h/bi1hcnJpdmluZy1h/dC1hLXRyb3BpY2Fs/LXJlc29ydC1mb3It/aGVyLXZhY2F0aW9u/LmpwZz9zPTYxMng2/MTImdz0wJms9MjAm/Yz1BT3N1eWltalZm/QkducTZZeVVhOVZ0/aXRQWE9rdWJOMDFq/TFp0R19lb1ZjPQ');
    (-5, 'tourist2', 'tourist', 2, true, 'https://imgs.search.brave.com/_8gIhJxRAq9aqREpTnh_wGcNfv4JwgEssYFmzKDWow8/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5nZXR0eWltYWdl/cy5jb20vaWQvMTMw/MTAwMzg3NC9waG90/by95b3VuZy13b21h/bi1hcnJpdmluZy1h/dC1hLXRyb3BpY2Fs/LXJlc29ydC1mb3It/aGVyLXZhY2F0aW9u/LmpwZz9zPTYxMng2/MTImdz0wJms9MjAm/Yz1BT3N1eWltalZm/QkducTZZeVVhOVZ0/aXRQWE9rdWJOMDFq/TFp0R19lb1ZjPQ');


-- ******************** SHOPPING CART *******************************
INSERT INTO payments."ShoppingCarts"(
	"Id", "TouristId", "TotalPrice", "IsPurchased")
	VALUES
		(-1, -168, 0, false),
		(-2, -169, 0, false),
		(-3, -171, 0, false),
		(-4, -172, 0, false),
		(-5, -3, 0, false);


-- ******************************** PEOPLE ********************************

INSERT INTO stakeholders."People" ("Id", "UserId", "Name", "Surname", "Email", "Bio", "Motto") VALUES (-169, -169, 'Filip', 'Simic', 'fsimic346@gmail.com', NULL, 'Brt, guglaj o.0'),
(-170, -170, 'Milos', 'Milutinovic', 'somi@gmail.com', NULL, NULL),
(-168, -168, 'Dragoslav', 'Maslac', 'gagi@gmail.com', NULL, NULL),
(-171, -171, 'Vlada', 'Devic', 'brmbrm@gmail.com', NULL, NULL),
(-172, -172, 'Mihajlo', 'Bukarica', 'buki@gmail.com', NULL, NULL),
(-1, -2, 'John', 'Johnson', 'author@gmail.com', 'I love making tours.', 'Never give up.'),
(-2, -3, 'Charles', 'Smith', 'tourist@gmail.com', 'I love tours.', 'Stay strong.'),
(-3, -4, 'Charles', 'Smith', 'tourist1@gmail.com', 'I love tours so much.', 'Stay.'),
(-3, -5, 'Charles', 'Smith', 'tourist2@gmail.com', 'I love tours so so much.', 'Stay.');

-- ***************** WALLETS **********************
INSERT INTO payments."Wallets"(
	"Id", "TouristId", "AdventureCoin")
	VALUES
		(-1, -3, 10000),
		(-1, -4, 10000),
		(-1, -5, 10000),
		(-2, -168, 200),
		(-3, -169, 500),
		(-4, -170, 400),
		(-5, -171, 800),
		(-6, -172, 150);

-- ******************************** FOLLOWERS ******************************

INSERT INTO stakeholders."Followers" ("Id", "UserId", "FollowedById") VALUES (-1, -170, -169),
(-2, -172, -169),
(-3, -171, -169),
(-4, -168, -169),
(-5, -169, -168),
(-6, -169, -170);

-- ******************************** BLOGS ********************************

INSERT INTO blog."Blogs" ("Id", "Title", "Description", "Date", "Status", "AuthorId", "Votes", "VisibilityPolicy", "ClubId") VALUES (-11, 'Off-the-Beaten-Path Travel', 'Discover charming villages and secret natural havens off the tourist path. Immerse yourself in the heart of local cultures and explore hidden treasures. Pack your bags for an authentic travel experience!

![Village Retreat](https://live.staticflickr.com/65535/53244354274_f096dbce42.jpg)

Join us on the road less traveled!', '2023-11-14 00:00:00+01', 3, -170, '[{"UserId": -168, "VoteType": 1}, {"UserId": -170, "VoteType": 1}]', 0, NULL),
(-12, 'Historical Wonders: Time Travel Edition', 'Step into history with ancient ruins and medieval castles. Explore the mysteries of past civilizations and witness the grandeur of medieval architecture. Join us on a captivating journey through time!

![Historical Landmarks](https://live.staticflickr.com/65535/53280758885_34afd10b3a.jpg)

Walk the corridors of history with us!
', '2023-11-14 00:00:00+01', 1, -169, '[{"UserId": -168, "VoteType": 0}, {"UserId": -170, "VoteType": 0}]', 0, NULL),
(-13, 'Urban Wanderlust: City Exploration', 'Delve into the heartbeat of vibrant cities! Explore bustling markets, iconic landmarks, and hidden gems within the urban landscape. Immerse yourself in the culture and energy of city life.

![City Exploration](https://live.staticflickr.com/65535/53308142182_84983c1cc3.jpg)

Experience the pulse of the city with us!', '2023-11-14 00:00:00+01', 0, -170, '[]', 0, NULL),
(-14, 'Team building grupa 1 (produžeci)', 'Nastavak kod Dragana na svirku i drinkić ![Team building](https://cdn.discordapp.com/attachments/1165638888082124852/1174054126708064327/IMG-20231113-WA0004.jpg?ex=6566321c&is=6553bd1c&hm=be7fc6baf1721ed5fcc861db7fb1692075556efcf6f95d12210aadbacda3cb8e&)', '2023-11-14 00:00:00+01', 1, -170, '[]', 0, NULL),
(-15, 'Team building grupa 1', 'Divno veče u ambijentu još divnijih ljudi huh ![Team building](https://cdn.discordapp.com/attachments/1165638888082124852/1174053744795734026/IMG-20231113-WA0006.jpg?ex=656631c1&is=6553bcc1&hm=ca8f361cba4201ae655e7465673fbb8b214aa835c5903a8b41ec3b2c299f2233&)', '2023-11-14 00:00:00+01', 1, -170, '[]', 0, NULL),
(-16, 'Winter trail Fruska gora', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae blandit massa. Integer eget pulvinar eros, nec convallis libero. Phasellus pretium porttitor lacinia. Nunc tincidunt, risus eu porta aliquet, justo ipsum pretium nulla, eu gravida purus dolor et nisl. Ut.', '2023-11-14 00:00:00+01', 1, -170, '[]', 0, NULL);


INSERT INTO blog."Comments" ("Id", "AuthorId", "BlogId", "CreatedAt", "UpdatedAt", "Text") VALUES (-1, -168, -11, '2023-11-14 13:49:10.108651+01', NULL, 'dobar blog!'),
(-2, -168, -11, '2023-11-14 13:49:23.737314+01', NULL, 'opet sam procitao, i jos je bolji...'),
(-4, -170, -14, '2023-11-14 13:49:10.108651+01', NULL, 'dobar blog!'),
(-5, -171, -14, '2023-11-14 13:58:16.459035+01', NULL, 'mene na pivo ne zovete, a?'),
(-6, -172, -14, '2023-11-14 13:49:10.108651+01', NULL, 'ni ja nisam pozvan 😔'),
(-3, -169, -14, '2023-11-14 13:58:16.459035+01', NULL, 'drugi put');

-- ************************** ENCOUNTERS *********************************

INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES (-9, 'Balans', 'Pojesti švarcvald balansaru za manje od 4 minute.', 19.8374354839325, 45.25227550147874, 50, 30, 0, 2, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES (-14, 'Bubi dupla', 'Pojesti bubi duplu', 19.842451214790348, 45.247939939289836, 50, 15, 0, 2, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES (-7, 'Plašenje gotičarki', 'Uraditi 20 sklekova bez majice ispred filozofskog fakulteta.', 19.853737950325012, 45.24668981590247, 30, 85, 0, 2, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES (-13, 'Pronaći krticu', 'Gde li se misteriozna krtica krtiči?', 19.84788537025452, 45.235828447337155, 50, 60, 0, 1, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES (-12, 'Bustovanje na Štrandiću', 'Pronaći lokaciju sa koje je slikan legendarni tripl Štrandić bust.', 19.849065542221073, 45.23699192743748, 50, 40, 0, 1, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES (-10, 'Električna vožnja', 'Sesti u električni bus broj 11.', 19.849312305450443, 45.24810611676206, 50, 12, 0, 2, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES (-11, 'Zaja', 'Poduplati sa prijateljima u zaji.', 19.849306941032413, 45.24610440123252, 50, 20, 0, 0, '[]');

INSERT INTO encounters."HiddenLocationEncounters" ("Id", "Picture", "PictureLongitude", "PictureLatitude") VALUES (-13, 'https://media.discordapp.net/attachments/783721881043206154/1182032447857242222/1701888891690.jpg', 19.847493767738346, 45.235973883652456);
INSERT INTO encounters."HiddenLocationEncounters" ("Id", "Picture", "PictureLongitude", "PictureLatitude") VALUES (-12, 'https://cdn.discordapp.com/attachments/783721881043206154/1182031344956604527/1701888624282.jpg', 19.849011898040775, 45.2370278136203);

INSERT INTO encounters."SocialEncounters" ("Id", "PeopleNumber") VALUES (-11, 1);

INSERT INTO encounters."MiscEncounters" ("Id", "ChallengeDone") VALUES (-7, false);
INSERT INTO encounters."MiscEncounters" ("Id", "ChallengeDone") VALUES (-10, false);
INSERT INTO encounters."MiscEncounters" ("Id", "ChallengeDone") VALUES (-9, false);
INSERT INTO encounters."MiscEncounters" ("Id", "ChallengeDone") VALUES (-14, false);

-- *********************** TOURIST_PROGRESS ******************************

INSERT INTO encounters."TouristProgress" ("Id", "UserId", "Xp", "Level") VALUES (-1, -169, 85, 12);

-- **************************** TOURS ************************************
INSERT INTO tours."Tours"(
	"Id", "AuthorId", "Name", "Description", "Difficulty", "Tags", "Status", "Price", "IsDeleted", "Distance", "PublishDate", "ArchiveDate", "Durations")
	VALUES
		(-1, -2, 'Šetnja pored Dunava', 'Tura po obali Dunava u Novom Sadu', 2, '{obala,reka}', 1, 100, FALSE, 2.1, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 20, "TransportType": 1}]'),
		(-2, -2, 'Obilazak centra', 'Tura po centru Novog Sada', 2, '{grad,istorija,centar}', 1, 100, FALSE, 0.36, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 30, "TransportType": 1}]'),
		(-3, -2, 'Novosadski Kulturni Prolaz', 'Upustite se u fascinantno putovanje kroz srce Vojvodine sa turom ''Novosadski Kulturni Prolaz''. Ova dinamična ruta vodi vas kroz bogatu istoriju i savremenu vibraciju Novog Sada, obuhvatajući ključne tačke kao što su monumentalna Petrovaradinska tvrđava, mirni Dunavski park i inspirativni Muzej savremene umetnosti Vojvodine. Saznajte ogradu oko tvrđave, uživajte u zelenim oazama Dunavskog parka i doživite umetnost na potpuno nov način u Muzeju savremene umetnosti. ''Novosadski Kulturni Prolaz'' pruža nezaboravno iskustvo koje istražuje spoj prošlosti, prirode i umetnosti u srcu ovog čarobnog grada.', 3, '{Umetnost,Muzej,Park,Istorija,Kultura}', 0, 169, false, 0, '-infinity', '-infinity', '[]');

INSERT INTO tours."KeyPoints"(
	"Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter")
	VALUES
		(-1, -1, 'Kej žrtava racije', 'U Novom Sadu, na keju koji danas nosi ime Kej žrtava racije, fašistički okupator je u takozvanoj „januarskoj raciji“ od 21. do 23. januara 1942. izvršio masovno streljanje više od hiljadu nedužnih građana Novog Sada.', 19.855529, 45.252574, 'Сунчани кеј, Нови Сад', '', 0, TRUE, '{"Description":"Spomenik je rad vajara Jovana Soldatovića i otkriven je 1971. godine."}', FALSE, FALSE),
		(-2, -1, 'Štrand', 'Štrand je popularna plaža u Novom Sadu. Nalazi se na Dunavu, u blizini Mosta slobode i važi za jednu od najuređenijih plaža na celom toku reke.', 19.847114, 45.236624, 'Сунчани кеј, Нови Сад', '', 1, TRUE, '{"Description":"Ulaz u plažu je besplatan preko godine."}', FALSE, FALSE),
		(-3, -2, 'Trg Svetozara Miletića', 'Svetozar Miletić (Mošorin, 22. februar 1826 — Vršac, 4. februar 1901) bio je advokat, političar i gradonačelnik Novog Sada. Miletić je bio jedan od najznačajnijih i najuticajnijih srpskih političara u Austrougarskoj druge polovine XIX veka.', 19.844811, 45.255049, 'Нови Сад', '', 0, TRUE, '{"Description":"Bio je predsednik Družine za ujedinjenje i oslobođenje Srbije sa sedištem na Cetinju, a tada mu je najbliži saradnik na Cetinju Aleksandar Sandić."}', FALSE, FALSE),
		(-4, -2, 'Trg Jovana Jovanovića Zmaja', 'Jovan Jovanović Zmaj (Novi Sad, 6. decembar 1833 — Sremska Kamenica, 14. jun 1904) bio je srpski pesnik, dramski pisac, prevodilac i lekar. Smatra se za jednog od najvećih liričara srpskog romantizma.', 19.847880, 45.256936, 'Змај Јовина 26, Нови Сад', '', 1, TRUE, '{"Description":"Njegove najznačajnije zbirke pesama su „Đulići“ i „Đulići uveoci“, prva o srećnom porodičnom životu, a druga o bolu za najmilijima."}', FALSE, FALSE),
		(-5, -3, 'Muzej savremene umetnosti Vojvodine', 'Ovaj muzej je epicentar savremene umetnosti u Novom Sadu. Njegova zbirka obuhvata dela domaćih i međunarodnih umetnika, pružajući posetiocima uvid u širok spektar umetničkih izraza. Prostori muzeja su moderni i inovativni, nudeći iskustvo koje istražuje granice umetnosti i kulture', 19.853335, 45.256402, 'Muzej savremene umetnosti', 'https://sremskevesti.rs/wp-content/uploads/2023/10/0033-1024x665-1-860x558.jpg', 0, true, '{"Images": [""], "Description": ""}', false, false),
		(-6, -3, 'Dunavski park', 'Dunavski park je oaza prirode usred grada, savršeno mesto za opuštanje i šetnju. Sa šarmantnim stazama, cvetnim alejama i jezerom, posetioci mogu uživati u mirnom okruženju. Park takođe često domaćin različitim kulturnim događajima, koncertima i umetničkim izložbama.', 19.850515, 45.25436, 'Dunavski park', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 1, true, '{"Images": [""], "Description": ""}', false, false),
		(-7, -3, 'Petrovaradinska tvrđava', 'Petrovaradinska tvrđava je monumentalna tvrđava koja dominira horizontom Novog Sada. Sa svojim impozantnim bastionima, kamenim zidinama i nezaboravnim pogledom na Dunav, tvrđava predstavlja istorijski biser. Posetioci mogu istražiti unikatne podzemne prolaze, uživati u pogledu sa čuvenih Petrovaradinskih satova i doživeti spoj prošlosti i savremene umetnosti tokom EXIT festivala koji se održava unutar tvrđave.', 19.861115, 45.253437, 'Petrovoradinska tvrđava', 'https://live.staticflickr.com/65535/50079707932_124f7b7949_b.jpg', 2, true, '{"Images": [""], "Description": ""}', false, false);

INSERT INTO tours."PublicKeyPoints" ("Id", "Name", "Description", "Longitude", "Latitude", "ImagePath", "Order", "LocationAddress") VALUES (-8, 'Tenk u parkiću', 'Dva srpska tenka jedan uz drugi, veoma blizu, čak i u kontaktu.', 19.853166, 45.256736, 'https://cdn.discordapp.com/attachments/783721881043206154/1187480628950728734/WhatsApp_Image_2023-12-21_at_13.12.05_3d992c74.jpg', 0, 'Park prisajedinjenja');
	
-- **************************** CLUBS ************************************
INSERT INTO stakeholders."Clubs"(
	"Id", "OwnerId", "Name", "Description", "Image")
	VALUES
		(-1, -3, 'Najjaci klub', 'Ovo je najjaci klub', ''),
		(-2, -4, 'Najjacii klubb', 'Ovo je najjaci klub', '');

INSERT INTO stakeholders."ClubMemberships"(
	"Id", "ClubId", "TouristId", "TimeJoined")
	VALUES
		(-1, -2, -5, '2023-12-04 19:04:04.562161+01'),
		(-2, -1, -5, '2023-12-04 19:04:04.562161+01'),
		(-3, -2, -3, '2023-12-04 19:04:04.562161+01');