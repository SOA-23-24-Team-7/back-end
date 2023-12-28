-- ******************************** USERS ********************************
-- Passwords for users:
-- dragan pass: dragan
--sima pass: sima
--somi pass: somi
--brmbrm pass: lozinka
--buki(k****) pass: lozinka
--dop pass: dop123
--author pass: author
--tourist pass- tourist
--veljko pass- veljko
--bosko pass- bosko
--mile pass- tourist 
--zora pass- tourist 
--luka pass- tourist 
INSERT INTO stakeholders."Users"(
	"Id", "Username", "Password", "Role", "IsActive","ProfilePicture")
	VALUES (-168, 'dragan', '$2a$12$wK5oRUsOmY.QZB2aNUTNweKfI3ZdYfxy8iAMcMEpa.pbaLvTXX70W', 2, 'True', 'https://www.espreso.co.rs/data/images/2023/01/17/12/1317487_sin-dragan_share.jpg'),
	(-169, 'sima', '$2a$12$gHLoc9WrjCdW0xDPAfdm0.aceGyqJaSemsk107tO.tcxAmYWeohgO', 2, 'True', 'https://media.discordapp.net/attachments/605735235476520972/1175913776986460180/1700430082262.jpg'),
	(-170, 'somi', '$2a$12$2TY7aJLFJts01ny.UyRMU.1V7NNWCbplb1UUPEx/x/Ya8yXihtDrm', 1, 'True', 'https://cdn.discordapp.com/attachments/1165638888082124852/1173981466925994095/image.png'),
	(-171, 'brmbrm', '$2a$12$0W/JBzV6WiDLyP.mU5ECn.LHZoUm6o/A7P9/oybd4wZdc4sfAt90m', 2, 'True', 'https://cdn.discordapp.com/attachments/1165638888082124852/1174057297807409222/1699987465617.jpg?ex=65663510&is=6553c010&hm=9e7d8441134deebb3d03cd901b291eb2eabccb325c856ff7ead67a8e508d74d5&'),
	(-172, 'buki(k****)', '$2a$12$0W/JBzV6WiDLyP.mU5ECn.LHZoUm6o/A7P9/oybd4wZdc4sfAt90m', 2, 'True', 'https://cdn.discordapp.com/attachments/1165638888082124852/1174057297484451890/1699987465609.jpg?ex=65663510&is=6553c010&hm=76cd42499b60ee0eeb1d21c1d7268158c9f6369dcbf1f5152e6fa8d15fb4359e&'),
    (-1, 'dop', '$2a$12$l/cp/IIoC4pj1CeCK28rH.h01LOND0TfGaQCJnJJsxwIyfMRgZunq', 0, true, ''),
    (-2, 'author', '$2a$12$lYBkk9QFF9LBNhrfsDDDeed5XSpTfMwlQmkrMZL3dpzXN2as8dx/O', 1, true, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw'),
    (-3, 'tourist', '$2a$12$DVn357kN0RhFGjVWFuWx7Oxd4sHrQ3oGEnJjkMdG1BhlNx7rPq3Fu', 2, true, 'https://imgs.search.brave.com/_8gIhJxRAq9aqREpTnh_wGcNfv4JwgEssYFmzKDWow8/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5nZXR0eWltYWdl/cy5jb20vaWQvMTMw/MTAwMzg3NC9waG90/by95b3VuZy13b21h/bi1hcnJpdmluZy1h/dC1hLXRyb3BpY2Fs/LXJlc29ydC1mb3It/aGVyLXZhY2F0aW9u/LmpwZz9zPTYxMng2/MTImdz0wJms9MjAm/Yz1BT3N1eWltalZm/QkducTZZeVVhOVZ0/aXRQWE9rdWJOMDFq/TFp0R19lb1ZjPQ'),
    (-4, 'veljko', '$2y$10$2i4JzB5nXynZATPKSIE5k.R3rdXplw09TaD9d.bNaVg.xCqNM90nu', 2, true, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw'),
    (-5, 'bosko', '$2y$10$BfKyogjaZczuKc/HuNm3Jeapm.4R5ycfcinL/ZwqTiSSFNXqREFjG', 2, true, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw'),
    (-6, 'mile', '$2a$12$DVn357kN0RhFGjVWFuWx7Oxd4sHrQ3oGEnJjkMdG1BhlNx7rPq3Fu', 2, true, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw'),
    (-7, 'zora', '$2a$12$DVn357kN0RhFGjVWFuWx7Oxd4sHrQ3oGEnJjkMdG1BhlNx7rPq3Fu', 2, true, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw'),
    (-8, 'luka', '$2a$12$DVn357kN0RhFGjVWFuWx7Oxd4sHrQ3oGEnJjkMdG1BhlNx7rPq3Fu', 2, true, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw');


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
(-2, -3, 'Charles', 'Smith', 'nikolicveljko01@gmail.com', 'I love tours.', 'Stay strong.'),
(-3, -4, 'Veljko', 'Nikolić', 'nikolicveljko01@gmail.com', 'I love tours.', 'Stay strong.'),
(-4, -5, 'Boško', 'Kulušić', 'kulusicbosko@gmail.com', 'I love tours.', 'Stay strong.'),
(-5, -6, 'Mile', 'Kitic', 'mile@gmail.com', 'I love tours.', 'Stay strong.'),
(-6, -7, 'Zora', 'Belic', 'zore@gmail.com', 'I love tours.', 'Stay strong.'),
(-7, -8, 'Lukas', 'Bale', 'nikola3444@gmail.com', 'I love tours.', 'Stay strong.');

-- ***************** WALLETS **********************
INSERT INTO payments."Wallets"(
	"Id", "TouristId", "AdventureCoin")
	VALUES
		(-1, -3, 10000),
		(-2, -168, 200),
		(-3, -169, 500),
		(-4, -170, 400),
		(-5, -171, 800),
		(-6, -172, 150),
		(-7, -4, 10000),
		(-8, -5, 10000),
		(-9, -6, 10000),
		(-10, -7, 10000),
		(-11, -8, 10000),
		(-12, -4, 10000),
		(-13, -5, 10000);

-- **************************** TOUR TOKENS ************************************
INSERT INTO payments."tourTokens"(
	"Id", "TourId", "TouristId")
	VALUES  (-1, -21, -3),
	 (-2, -21, -169),
	 (-3, -21, -6),
	 (-4, -21, -7),
	 (-5, -21, -8),
	 (-6, -22, -3),
	 (-7, -22, -6),
	 (-8, -22, -7),
	 (-9, -25, -8),
	 (-10, -27, -3),
	 (-11, -27, -169),
	 (-12, -21, -4),
   (-13, -21, -5),
	 (-14, -29, -4),
	 (-15, -29, -5);

-- ******************************** FOLLOWERS ******************************

INSERT INTO stakeholders."Followers" ("Id", "UserId", "FollowedById") VALUES (-1, -170, -169),
(-2, -172, -169),
(-3, -171, -169),
(-4, -168, -169),
(-5, -169, -168),
(-6, -169, -170);

-- ******************************** BLOGS ********************************

INSERT INTO blog."Blogs" ("Id", "Title", "Description", "Date", "Status", "AuthorId", "Votes", "VisibilityPolicy") VALUES (-11, 'Off-the-Beaten-Path Travel', '<p>Discover charming villages and secret natural havens off the tourist path. Immerse yourself in the heart of local cultures and explore hidden treasures. Pack your bags for an authentic travel experience!</p><p><br></p><p><img src="https://live.staticflickr.com/65535/53244354274_f096dbce42.jpg"></p><p><br></p><p>Join us on the road less traveled!</p>', '2023-11-14 00:00:00+01', 3, -170, '[{"UserId": -168, "VoteType": 1}, {"UserId": -170, "VoteType": 1}]', 0),
(-12, 'Historical Wonders: Time Travel Edition', '<p>Step into history with ancient ruins and medieval castles. Explore the mysteries of past civilizations and witness the grandeur of medieval architecture. Join us on a captivating journey through time!</p><p><br></p><p><img src="https://live.staticflickr.com/65535/53280758885_34afd10b3a.jpg"></p><p><br></p><p>Walk the corridors of history with us!</p>', '2023-11-14 00:00:00+01', 1, -169, '[{"UserId": -168, "VoteType": 0}, {"UserId": -170, "VoteType": 0}]', 0),
(-13, 'Urban Wanderlust: City Exploration', '<p>Delve into the heartbeat of vibrant cities! Explore bustling markets, iconic landmarks, and hidden gems within the urban landscape.</p><p>Immerse yourself in the culture and energy of city life.</p><p><br></p><p><img src="https://live.staticflickr.com/65535/53308142182_84983c1cc3.jpg"></p><p><br></p><p>Experience the pulse of the city with us!</p>', '2023-11-14 00:00:00+01', 0, -170, '[]', 0),
(-14, 'Team building grupa 1 (produžeci)', '<p>Nastavak kod Dragana na svirku i drinkić</p><p><br></p><p><img src="https://cdn.discordapp.com/attachments/1165638888082124852/1174054126708064327/IMG-20231113-WA0004.jpg"></p>', '2023-11-14 00:00:00+01', 1, -170, '[]', 0),
(-15, 'Team building grupa 1', '<p>Divno veče u ambijentu još divnijih ljudi huh</p><p><br></p><p><img src="https://cdn.discordapp.com/attachments/1165638888082124852/1174053744795734026/IMG-20231113-WA0006.jpg"></p>', '2023-11-14 00:00:00+01', 1, -170, '[]', 0);


INSERT INTO blog."Comments" ("Id", "AuthorId", "BlogId", "CreatedAt", "UpdatedAt", "Text") VALUES (-1, -168, -11, '2023-11-14 13:49:10.108651+01', NULL, 'dobar blog!'),
(-2, -168, -11, '2023-11-14 13:49:23.737314+01', NULL, 'opet sam procitao, i jos je bolji...'),
(-4, -170, -14, '2023-11-14 13:49:10.108651+01', NULL, 'dobar blog!'),
(-5, -171, -14, '2023-11-14 13:58:16.459035+01', NULL, 'mene na pivo ne zovete, a?'),
(-6, -172, -14, '2023-11-14 13:49:10.108651+01', NULL, 'ni ja nisam pozvan 😔'),
(-3, -169, -14, '2023-11-14 13:58:16.459035+01', NULL, 'drugi put');

-- ************************** ENCOUNTERS *********************************

INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Picture", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES 
(-9, 'Balans', 'Pojesti švarcvald balansaru za manje od 4 minute.', 'https://scontent.fbeg7-2.fna.fbcdn.net/v/t1.6435-9/95715864_2058813777577124_6055717451419615232_n.jpg?_nc_cat=107&ccb=1-7&_nc_sid=c2f564&_nc_ohc=7hGa_upOwRkAX-6dADQ&_nc_ht=scontent.fbeg7-2.fna&oh=00_AfDO1JahhT56YrE_KmmDL760vLpX-dd1qzgHSmbL6OGG2A&oe=65B3F22E', 19.8374354839325, 45.25227550147874, 50, 30, 0, 2, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Picture", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES 
(-14, 'Bubi dupla', 'Pojesti bubi duplu', 'https://bubi.rs/wp-content/uploads/2022/12/banner2-scaled.jpg', 19.842451214790348, 45.247939939289836, 50, 15, 0, 2, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Picture", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES 
(-7, 'Plašenje gotičarki', 'Uraditi 20 sklekova bez majice ispred filozofskog fakulteta.', 'https://i.ytimg.com/vi/kQ0vSvFKtJU/maxresdefault.jpg', 19.853737950325012, 45.24668981590247, 30, 85, 0, 2, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Picture", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES 
(-13, 'Pronaći krticu', 'Gde li se misteriozna krtica krtiči?', 'https://media.discordapp.net/attachments/783721881043206154/1182032447857242222/1701888891690.jpg', 19.84788537025452, 45.235828447337155, 50, 60, 0, 1, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Picture", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES 
(-12, 'Bustovanje na Štrandiću', 'Pronaći lokaciju sa koje je slikan legendarni tripl Štrandić bust.', 'https://cdn.discordapp.com/attachments/783721881043206154/1182031344956604527/1701888624282.jpg', 19.849065542221073, 45.23699192743748, 50, 40, 0, 1, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Picture", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES 
(-10, 'Električna vožnja', 'Sesti u električni bus broj 11.', 'https://www.gradnja.rs/wp-content/uploads/2023/06/solaris-urbino12-electric-jgsp-15.jpg', 19.849312305450443, 45.24810611676206, 50, 12, 0, 2, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Picture", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES 
(-11, 'Zaja', 'Poduplati sa prijateljima u zaji.', 'https://oradio.rs/files/uploads/2019/11/mala-menza.jpg', 19.849306941032413, 45.24610440123252, 50, 20, 0, 0, '[]');
INSERT INTO encounters."Encounters" ("Id", "Title", "Description", "Picture", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances") VALUES 
(-15, 'Lepa kuca', 'Gadjaj prozor kamenom', 'https://media.discordapp.net/attachments/783721881043206154/1182032447857242222/1701888891690.jpg', 19.807555675506595, 45.27031287156058, 50, 50, 0, 3, '[{"Status": 1, "UserId": -5, "CompletionTime": "2023-12-16T18:44:38.5492356Z"}, {"Status": 0, "UserId": -4, "CompletionTime": null}]');
INSERT INTO encounters."HiddenLocationEncounters" ("Id", "PictureLongitude", "PictureLatitude") VALUES (-13, 19.847493767738346, 45.235973883652456);
INSERT INTO encounters."HiddenLocationEncounters" ("Id", "PictureLongitude", "PictureLatitude") VALUES (-12, 19.849011898040775, 45.2370278136203);
INSERT INTO encounters."SocialEncounters" ("Id", "PeopleNumber") VALUES (-11, 1);
INSERT INTO encounters."MiscEncounters" ("Id", "ChallengeDone") VALUES (-7, false);
INSERT INTO encounters."MiscEncounters" ("Id", "ChallengeDone") VALUES (-10, false);
INSERT INTO encounters."MiscEncounters" ("Id", "ChallengeDone") VALUES (-9, false);
INSERT INTO encounters."MiscEncounters" ("Id", "ChallengeDone") VALUES (-14, false);
INSERT INTO encounters."KeyPointEncounter" ("Id", "KeyPointId") VALUES (-15, -59);

-- *********************** TOURIST_PROGRESS ******************************

INSERT INTO encounters."TouristProgress" ("Id", "UserId", "Xp", "Level") VALUES (-1, -169, 85, 12);

-- **************************** TOURS ************************************
INSERT INTO tours."Tours"(
	"Id", "AuthorId", "Category", "Name", "Description", "Difficulty", "Tags", "Status", "Price", "IsDeleted", "Distance", "PublishDate", "ArchiveDate", "Durations")
	VALUES
		(-1, -2, 2, 'Šetnja pored Dunava', 'Tura po obali Dunava u Novom Sadu', 2, '{obala,reka}', 1, 100, FALSE, 2.1, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 20, "TransportType": 1}]'),
		(-2, -2, 2, 'Obilazak centra', 'Tura po centru Novog Sada', 2, '{grad,istorija,centar}', 1, 100, FALSE, 0.36, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 30, "TransportType": 1}]'),
		(-3, -2, 2, 'Novosadski Kulturni Prolaz', 'Upustite se u fascinantno putovanje kroz srce Vojvodine sa turom ''Novosadski Kulturni Prolaz''. Ova dinamična ruta vodi vas kroz bogatu istoriju i savremenu vibraciju Novog Sada, obuhvatajući ključne tačke kao što su monumentalna Petrovaradinska tvrđava, mirni Dunavski park i inspirativni Muzej savremene umetnosti Vojvodine. Saznajte ogradu oko tvrđave, uživajte u zelenim oazama Dunavskog parka i doživite umetnost na potpuno nov način u Muzeju savremene umetnosti. ''Novosadski Kulturni Prolaz'' pruža nezaboravno iskustvo koje istražuje spoj prošlosti, prirode i umetnosti u srcu ovog čarobnog grada.', 3, '{Umetnost,Muzej,Park,Istorija,Kultura}', 1, 169, false, 0, '-infinity', '-infinity', '[]'),

		(-20, -2, 3, 'Obilazak centra', 'Tura po centru Novog Sada', 2, '{grad,istorija,centar}', 1, 100, FALSE, 0.36, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 30, "TransportType": 1}]'),
		(-21, -2, 3, 'Muzeji Novog Sada', 'Obilazak muzeja Novog Sada', 3, '{Umetnost,Muzej,Istorija,Kultura}', 1, 340, false, 1.7, '-infinity', '-infinity', '[]'),
		(-22, -2, 3, 'Istorija Novog Sada', 'Istorijski pogled na grad.', 3, '{Istorija,Kultura,Setnja}', 1, 150, false, 2.3, '-infinity', '-infinity', '[]'),
		(-23, -2, 1, 'Parkovi Novog Sada', 'Obilazag parkova Novog Sada.', 3, '{Park,Kultura,Aktivnost,Setnja}', 1, 55, false, 0.4, '-infinity', '-infinity', '[]'),
		(-24, -2, 1, 'Evropska prestonica kulture', 'Obilazak znacajnih mesta u gradu', 3, '{Umetnost,Kultura}', 1, 100, false, 1.6, '-infinity', '-infinity', '[]'),		
		(-25, -2, 2, 'Obilazak restorana Novog Sada', 'Probajte specijalitete Novog Sada u popularnim restoranima.', 3, '{Gastronomija,Hrana}', 1, 280, false, 3.1, '-infinity', '-infinity', '[]'),
		(-26, -2, 2, 'Vinarije u Novom Sadu', 'Probajte super vina.', 2, '{Gastronomija,Vino}', 1, 40, false, 0.2, '-infinity', '-infinity', '[]'),
		(-27, -2, 0, 'Izlasci u Novom Sadu', 'Posetite popularne pubove.', 5, '{Umetnost,Muzej,Park,Istorija,Kultura}', 1, 190, false, 0.9, '-infinity', '-infinity', '[]'),
		(-28, -2, 3, 'Setnja gradom', 'Protegnite noge u Novom Sadu.', 1, '{Setnja,Park,Aktivnost}', 1, 230, false, 1.3, '-infinity', '-infinity', '[]'),
		(-29, -2, 3, 'Tura sa enkaunterom', 'Neki opis vau', 4, '{kultura,arhitektura}', 1, 250, false, 7.86, '2023-12-27 21:55:47.844174+01', '-infinity', '[{"Duration": 176, "TransportType": 0}, {"Duration": 54, "TransportType": 2}]');


-- **************************** REVIEWS ************************************
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-1, 5, 'Great and exciting tour.', -168, '2023-10-15', '2023-10-16', -1, ARRAY['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-2, 1, 'Not interesting at all.', -169, '2023-10-11', '2023-10-15', -2, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=','https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-3, 3, 'OK tour.', -168, '2023-10-10', '2023-10-16', -3, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);

INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-21, 5, 'OK tour.', -168, '2023-10-10', '2023-10-16', -21, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-22, 5, 'OK tour.', -168, '2023-10-10', '2023-10-16', -21, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-23, 5, 'OK tour.', -168, '2023-10-10', '2023-10-16', -21, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-24, 5, 'OK tour.', -168, '2023-10-10', '2023-10-16', -21, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-25, 5, 'OK tour.', -168, '2023-10-10', '2023-10-16', -21, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-26, 5, 'OK tour.', -168, '2023-10-10', '2023-10-16', -21, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-27, 5, 'OK tour.', -168, '2023-10-10', '2023-10-16', -21, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-28, 5, 'OK tour.', -168, '2023-10-10', '2023-10-16', -21, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);

INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-29, 5, 'OK tour.', -168, '2023-10-10', '2023-12-26', -28, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-30, 4, 'OK tour.', -168, '2023-10-10', '2023-12-26', -27, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-31, 5, 'OK tour.', -168, '2023-10-10', '2023-12-26', -27, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-32, 5, 'OK tour.', -168, '2023-10-10', '2023-12-26', -24, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-33, 4, 'OK tour.', -168, '2023-10-10', '2023-12-26', -24, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-34, 5, 'OK tour.', -3, '2023-10-10', '2023-12-26', -24, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);


-- **************************** KEY POINTS ************************************

INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-6, -3, 'Dunavski park', 'Dunavski park je oaza prirode usred grada, savršeno mesto za opuštanje i šetnju. Sa šarmantnim stazama, cvetnim alejama i jezerom, posetioci mogu uživati u mirnom okruženju. Park takođe često domaćin različitim kulturnim događajima, koncertima i umetničkim izložbama.', 19.850515, 45.25436, 'Dunavski park', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 1, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-43, -21, 'Mileticev Spomenik', 'Lorem ipsum.', 19.855529, 45.252574, 'Сунчани кеј, Нови Сад', 'https://sremskevesti.rs/wp-content/uploads/2023/10/0033-1024x665-1-860x558.jpg', 0, true, '{"Description": "Spomenik je rad vajara Jovana Soldatovića i otkriven je 1971. godine."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-44, -22, 'Saborna crkva', 'Lorem ipsum.', 19.847114, 45.236624, 'Сунчани кеј, Нови Сад', 'https://www.funtravelnis.rs/wp-content/uploads/2017/11/trebinje-4607172__340.jpg', 1, true, '{"Description": "Ulaz u plažu je besplatan preko godine."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-45, -22, 'Matica srpska', 'Lorem ipsum.', 19.844811, 45.255049, 'Нови Сад', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 0, true, '{"Description": "Bio je predsednik Družine za ujedinjenje i oslobođenje Srbije sa sedištem na Cetinju, a tada mu je najbliži saradnik na Cetinju Aleksandar Sandić."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-46, -23, 'Dunavski park', 'Lorem ipsum.', 19.84788, 45.256936, 'Змај Јовина 26, Нови Сад', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 1, true, '{"Description": "Njegove najznačajnije zbirke pesama su „Đulići“ i „Đulići uveoci“, prva o srećnom porodičnom životu, a druga o bolu za najmilijima."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-47, -23, 'Futoski park', 'Lorem ipsum.', 19.853335, 45.256402, 'Muzej savremene umetnosti', 'https://chasingthedonkey.b-cdn.net/wp-content/uploads/2018/09/Novi-Sad-Serbia_shutterstock_3285831_eTbtl.jpg', 0, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-4, -2, 'Trg Jovana Jovanovića Zmaja', 'Jovan Jovanović Zmaj (Novi Sad, 6. decembar 1833 — Sremska Kamenica, 14. jun 1904) bio je srpski pesnik, dramski pisac, prevodilac i lekar. Smatra se za jednog od najvećih liričara srpskog romantizma.', 19.84788, 45.256936, 'Змај Јовина 26, Нови Сад', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 0, true, '{"Description": "Njegove najznačajnije zbirke pesama su „Đulići“ i „Đulići uveoci“, prva o srećnom porodičnom životu, a druga o bolu za najmilijima."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-3, -2, 'Trg Svetozara Miletića', 'Svetozar Miletić (Mošorin, 22. februar 1826 — Vršac, 4. februar 1901) bio je advokat, političar i gradonačelnik Novog Sada. Miletić je bio jedan od najznačajnijih i najuticajnijih srpskih političara u Austrougarskoj druge polovine XIX veka.', 19.844811, 45.255049, 'Нови Сад', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 1, true, '{"Description": "Bio je predsednik Družine za ujedinjenje i oslobođenje Srbije sa sedištem na Cetinju, a tada mu je najbliži saradnik na Cetinju Aleksandar Sandić."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-2, -1, 'Štrand', 'Štrand je popularna plaža u Novom Sadu. Nalazi se na Dunavu, u blizini Mosta slobode i važi za jednu od najuređenijih plaža na celom toku reke.', 19.847114, 45.236624, 'Сунчани кеј, Нови Сад', 'https://live.staticflickr.com/65535/50079707932_124f7b7949_b.jpg', 0, true, '{"Description": "Ulaz u plažu je besplatan preko godine."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-1, -1, 'Kej žrtava racije', 'U Novom Sadu, na keju koji danas nosi ime Kej žrtava racije, fašistički okupator je u takozvanoj „januarskoj raciji“ od 21. do 23. januara 1942. izvršio masovno streljanje više od hiljadu nedužnih građana Novog Sada.', 19.855529, 45.252574, 'Сунчани кеј, Нови Сад', 'https://sremskevesti.rs/wp-content/uploads/2023/10/0033-1024x665-1-860x558.jpg', 1, true, '{"Description": "Spomenik je rad vajara Jovana Soldatovića i otkriven je 1971. godine."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-48, -24, 'KC lab', 'Lorem ipsum.', 19.850515, 45.25436, 'Dunavski park', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 1, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-57, -28, 'Katedrala', 'Lorem ipsum', 19.861115, 45.253437, 'Petrovoradinska tvrđava', 'https://www.funtravelnis.rs/wp-content/uploads/2017/11/trebinje-4607172__340.jpg', 0, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-56, -28, 'Petrovaradinska tvrđava', 'Petrovaradinska tvrđava je monumentalna tvrđava koja dominira horizontom Novog Sada. Sa svojim impozantnim bastionima, kamenim zidinama i nezaboravnim pogledom na Dunav, tvrđava predstavlja istorijski biser. Posetioci mogu istražiti unikatne podzemne prolaze, uživati u pogledu sa čuvenih Petrovaradinskih satova i doživeti spoj prošlosti i savremene umetnosti tokom EXIT festivala koji se održava unutar tvrđave.', 19.861115, 45.253437, 'Petrovoradinska tvrđava', 'https://www.funtravelnis.rs/wp-content/uploads/2017/11/trebinje-4607172__340.jpg', 1, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-55, -27, 'Splavovi na Ribarcu', 'Lorem ipsum.', 19.850515, 45.25436, 'Dunavski park', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 0, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-54, -27, 'Laze Teleckog', 'Lorem ipsum.', 19.853335, 45.256402, 'Muzej savremene umetnosti', 'https://chasingthedonkey.b-cdn.net/wp-content/uploads/2018/09/Novi-Sad-Serbia_shutterstock_3285831_eTbtl.jpg', 1, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-53, -26, 'Karlovacke vinarije', 'Lorem ipsum.', 19.84788, 45.256936, 'Змај Јовина 26, Нови Сад', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 0, true, '{"Description": "Njegove najznačajnije zbirke pesama su „Đulići“ i „Đulići uveoci“, prva o srećnom porodičnom životu, a druga o bolu za najmilijima."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-52, -26, 'Vinarija Kovacevic', 'Lorem ipsum.', 19.844811, 45.255049, 'Нови Сад', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 1, true, '{"Description": "Bio je predsednik Družine za ujedinjenje i oslobođenje Srbije sa sedištem na Cetinju, a tada mu je najbliži saradnik na Cetinju Aleksandar Sandić."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-51, -25, 'Grcki giros', 'Stadion Vojvodine.', 19.847114, 45.236624, 'Сунчани кеј, Нови Сад', 'https://www.funtravelnis.rs/wp-content/uploads/2017/11/trebinje-4607172__340.jpg', 0, true, '{"Description": "Ulaz u plažu je besplatan preko godine."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-50, -25, 'Plava frajla', 'Lorem ipsum.', 19.855529, 45.252574, 'Сунчани кеј, Нови Сад', 'https://sremskevesti.rs/wp-content/uploads/2023/10/0033-1024x665-1-860x558.jpg', 1, true, '{"Description": "Spomenik je rad vajara Jovana Soldatovića i otkriven je 1971. godine."}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-49, -24, 'Petrovaradinska tvrđava', 'Petrovaradinska tvrđava je monumentalna tvrđava koja dominira horizontom Novog Sada. Sa svojim impozantnim bastionima, kamenim zidinama i nezaboravnim pogledom na Dunav, tvrđava predstavlja istorijski biser. Posetioci mogu istražiti unikatne podzemne prolaze, uživati u pogledu sa čuvenih Petrovaradinskih satova i doživeti spoj prošlosti i savremene umetnosti tokom EXIT festivala koji se održava unutar tvrđave.', 19.861115, 45.253437, 'Petrovoradinska tvrđava', 'https://www.funtravelnis.rs/wp-content/uploads/2017/11/trebinje-4607172__340.jpg', 0, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-42, -21, 'Muzej Vojvodine', 'Lorem ipsum.', 19.861115, 45.253437, 'Petrovoradinska tvrđava', 'https://www.funtravelnis.rs/wp-content/uploads/2017/11/trebinje-4607172__340.jpg', 1, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-7, -3, 'Petrovaradinska tvrđava', 'Petrovaradinska tvrđava je monumentalna tvrđava koja dominira horizontom Novog Sada. Sa svojim impozantnim bastionima, kamenim zidinama i nezaboravnim pogledom na Dunav, tvrđava predstavlja istorijski biser. Posetioci mogu istražiti unikatne podzemne prolaze, uživati u pogledu sa čuvenih Petrovaradinskih satova i doživeti spoj prošlosti i savremene umetnosti tokom EXIT festivala koji se održava unutar tvrđave.', 19.861115, 45.253437, 'Petrovoradinska tvrđava', 'https://live.staticflickr.com/65535/50079707932_124f7b7949_b.jpg', 0, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-5, -3, 'Muzej savremene umetnosti Vojvodine', 'Ovaj muzej je epicentar savremene umetnosti u Novom Sadu. Njegova zbirka obuhvata dela domaćih i međunarodnih umetnika, pružajući posetiocima uvid u širok spektar umetničkih izraza. Prostori muzeja su moderni i inovativni, nudeći iskustvo koje istražuje granice umetnosti i kulture', 19.853335, 45.256402, 'Muzej savremene umetnosti', 'https://sremskevesti.rs/wp-content/uploads/2023/10/0033-1024x665-1-860x558.jpg', 2, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-60, -29, 'Prva kljucna tacka', 'Ovde zivi najveca legenda', 19.835568666458133, 45.242893955461476, '54  Bulevar Cara Lazara  Novi Sad  21102  Serbia', 'https://www.funtravelnis.rs/wp-content/uploads/2017/11/trebinje-4607172__340.jpg', 0, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-59, -29, 'Druga kljucna tacka', 'Ovde ne zivi legenda', 19.807555675506595, 45.27031287156058, '4  Mihala Babinke  Novi Sad  21113  Serbia', 'https://live.staticflickr.com/65535/50079707932_124f7b7949_b.jpg', 1, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-58, -29, 'Treca kljucna tacka', 'Opis', 19.837945103645325, 45.25096504850316, '91  Bulevar oslobodjenja  Novi Sad  21000  Serbia', 'https://sremskevesti.rs/wp-content/uploads/2023/10/0033-1024x665-1-860x558.jpg', 2, true, '{"Images": [""], "Description": ""}', false, false);
INSERT INTO tours."KeyPoints" ("Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter") 
VALUES (-8, -3, 'Ljuljaška u parku', '6 bikova na jednoj ljuji, nije se moglo dobro završiti...', 19.861115, 45.253437, 'Ljuljaška', 'attachments', 3, false, '{"Images": [""], "Description": ""}', false, false);

INSERT INTO tours."PublicKeyPoints" ("Id", "Name", "Description", "Longitude", "Latitude", "ImagePath", "Order", "LocationAddress") VALUES (-8, 'Tenk u parkiću', 'Dva srpska tenka jedan uz drugi, veoma blizu, čak i u kontaktu.', 19.853166, 45.256736, 'https://cdn.discordapp.com/attachments/783721881043206154/1187480628950728734/WhatsApp_Image_2023-12-21_at_13.12.05_3d992c74.jpg', 0, 'Park prisajedinjenja');
INSERT INTO tours."PublicKeyPointRequests"(
	"Id", "KeyPointId", "Status", "Comment", "AuthorId", "Created", "AuthorName")
	VALUES (-1, -8, 0, '', -2, '2023-10-10', 'author');

-- **************************** FACILITIES ************************************

INSERT INTO tours."Facilities" ("Id", "Name", "Description", "ImagePath", "AuthorId", "Category", "Longitude", "Latitude") VALUES(-1, 'Gradska Pivnica', 'Naša pivnica nastavlja tradiciju započetu davne 1748. godine. Predstavljamo svojevrsnu pivsku oazu i nalazimo se u samom srcu Novog Sada', 'https://www.mojnovisad.com/files/news/7/8/6/23786/23786-gradska-pivnica02.jpg', '-2', 0, 19.844693541526798, 45.255957448145146);
INSERT INTO tours."PublicFacilityRequests" ("Id", "FacilityId", "Status", "Comment", "AuthorId", "Created", "AuthorName") VALUES(-1, -1, 0, NULL, -2, '2023-12-27 21:14:30.265118+01', 'John Johnson');


-- **************************** COUPONS ************************************

INSERT INTO payments."Coupons"(
	"Id", "Code", "Discount", "TourId", "ExpirationDate", "AllFromAuthor", "AuthorId")
	VALUES (-1, 'ZXItNZq3', 20, -21, '2023-12-29 01:00:00+01', true, -2);

-- **************************** TOUR EXECUTION SESSIONS ************************************

INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-1, 1, -21, -3, -42, 0, '2023-12-27 20:51:32.625288+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-2, 2, -21, -3, -1, 100, '2023-12-27 20:51:57.128909+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-3, 1, -21, -3, -42, 0, '2023-12-27 20:53:15.529578+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-4, 1, -21, -4, -42, 30.189313977970922, '2023-12-27 21:00:31.903404+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-5, 2, -21, -5, -1, 100, '2023-12-27 21:03:50.916984+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-6, 1, -21, -6, -42, 54.57624154320444, '2023-12-27 21:05:33.086618+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-7, 2, -21, -7, -1, 100, '2023-12-27 21:08:40.139779+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-8, 1, -21, -8, -42, 13.202754417429771, '2023-12-27 21:11:01.552798+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-10, 2, -29, -5, -1, 100, '2023-12-27 20:51:32.625288+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-9, 1, -29, -4, -58, 55, '2023-12-27 20:51:32.625288+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-11, 2, -22, -3, -1, 100, '2023-12-26 00:00:00+01', false);
INSERT INTO tours."TourExecutionSessions" ("Id", "Status", "TourId", "TouristId", "NextKeyPointId", "Progress", "LastActivity", "IsCampaign") VALUES (-12, 2, -25, -169, -1, 100, '2023-12-26 00:00:00+01', false);

-- **************************** TOUR SALES ************************************

INSERT INTO payments."TourSales"("Id","AuthorId", "Name", "StartDate","EndDate","DiscountPercentage","TourIds") VALUES (-1, -2, 'Popust', '2023-12-27', '2023-12-31', 0.2,  '[-24]')