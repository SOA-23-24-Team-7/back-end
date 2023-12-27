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
    (-4, 'veljko', '$2y$10$2i4JzB5nXynZATPKSIE5k.R3rdXplw09TaD9d.bNaVg.xCqNM90nu', 2, true, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw');
    (-5, 'bosko', '$2y$10$BfKyogjaZczuKc/HuNm3Jeapm.4R5ycfcinL/ZwqTiSSFNXqREFjG', 2, true, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw');


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
(-2, -4, 'Veljko', 'Nikolić', 'nikolicveljko01@gmail.com', 'I love tours.', 'Stay strong.');
(-2, -5, 'Boško', 'Kulušić', 'kulusicbosko@gmail.com', 'I love tours.', 'Stay strong.');

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

INSERT INTO blog."Blogs" ("Id", "Title", "Description", "Date", "Status", "AuthorId", "Votes", "VisibilityPolicy") VALUES (-11, 'Off-the-Beaten-Path Travel', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed pulvinar proin gravida hendrerit lectus. Gravida dictum fusce ut placerat orci nulla. Lectus vestibulum mattis ullamcorper velit sed ullamcorper morbi tincidunt. Quis blandit turpis cursus in hac habitasse platea dictumst. Dolor sit amet consectetur adipiscing elit ut. Etiam tempor orci eu lobortis elementum nibh tellus molestie nunc. Nunc sed augue lacus viverra vitae. Sed libero enim sed faucibus. Placerat vestibulum lectus mauris ultrices eros in. Vel eros donec ac odio tempor orci. Et pharetra pharetra massa massa. Gravida arcu ac tortor dignissim convallis aenean. Tempor commodo ullamcorper a lacus vestibulum.', '2023-11-14 00:00:00+01', 3, -170, '[{"UserId": -168, "VoteType": 1}, {"UserId": -170, "VoteType": 1}]', 0),
(-12, 'Historical Wonders: Time Travel Edition', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed pulvinar proin gravida hendrerit lectus. Gravida dictum fusce ut placerat orci nulla. Lectus vestibulum mattis ullamcorper velit sed ullamcorper morbi tincidunt. Quis blandit turpis cursus in hac habitasse platea dictumst. Dolor sit amet consectetur adipiscing elit ut. Etiam tempor orci eu lobortis elementum nibh tellus molestie nunc. Nunc sed augue lacus viverra vitae. Sed libero enim sed faucibus. Placerat vestibulum lectus mauris ultrices eros in. Vel eros donec ac odio tempor orci. Et pharetra pharetra massa massa. Gravida arcu ac tortor dignissim convallis aenean. Tempor commodo ullamcorper a lacus vestibulum.', '2023-11-14 00:00:00+01', 1, -169, '[{"UserId": -168, "VoteType": 0}, {"UserId": -170, "VoteType": 0}]', 0),
(-13, 'Urban Wanderlust: City Exploration', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed pulvinar proin gravida hendrerit lectus. Gravida dictum fusce ut placerat orci nulla. Lectus vestibulum mattis ullamcorper velit sed ullamcorper morbi tincidunt. Quis blandit turpis cursus in hac habitasse platea dictumst. Dolor sit amet consectetur adipiscing elit ut. Etiam tempor orci eu lobortis elementum nibh tellus molestie nunc. Nunc sed augue lacus viverra vitae. Sed libero enim sed faucibus. Placerat vestibulum lectus mauris ultrices eros in. Vel eros donec ac odio tempor orci. Et pharetra pharetra massa massa. Gravida arcu ac tortor dignissim convallis aenean. Tempor commodo ullamcorper a lacus vestibulum.', '2023-11-14 00:00:00+01', 0, -170, '[]', 0),
(-14, 'Team building grupa 1 (produžeci)', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed pulvinar proin gravida hendrerit lectus. Gravida dictum fusce ut placerat orci nulla. Lectus vestibulum mattis ullamcorper velit sed ullamcorper morbi tincidunt. Quis blandit turpis cursus in hac habitasse platea dictumst. Dolor sit amet consectetur adipiscing elit ut. Etiam tempor orci eu lobortis elementum nibh tellus molestie nunc. Nunc sed augue lacus viverra vitae. Sed libero enim sed faucibus. Placerat vestibulum lectus mauris ultrices eros in. Vel eros donec ac odio tempor orci. Et pharetra pharetra massa massa. Gravida arcu ac tortor dignissim convallis aenean. Tempor commodo ullamcorper a lacus vestibulum.', '2023-11-14 00:00:00+01', 1, -170, '[]', 0),
(-15, 'Team building grupa 1', 'Divno veče u ambijentu još divnijih ljudi huh', '2023-11-14 00:00:00+01', 1, -170, '[]', 0);


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
	"Id", "AuthorId", "Category", "Name", "Description", "Difficulty", "Tags", "Status", "Price", "IsDeleted", "Distance", "PublishDate", "ArchiveDate", "Durations")
	VALUES
		(-1, -2, 2, 'Šetnja pored Dunava', 'Tura po obali Dunava u Novom Sadu', 2, '{obala,reka}', 1, 100, FALSE, 2.1, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 20, "TransportType": 1}]'),
		(-2, -2, 2, 'Obilazak centra', 'Tura po centru Novog Sada', 2, '{grad,istorija,centar}', 1, 100, FALSE, 0.36, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 30, "TransportType": 1}]'),
		(-3, -2, 2, 'Novosadski Kulturni Prolaz', 'Upustite se u fascinantno putovanje kroz srce Vojvodine sa turom ''Novosadski Kulturni Prolaz''. Ova dinamična ruta vodi vas kroz bogatu istoriju i savremenu vibraciju Novog Sada, obuhvatajući ključne tačke kao što su monumentalna Petrovaradinska tvrđava, mirni Dunavski park i inspirativni Muzej savremene umetnosti Vojvodine. Saznajte ogradu oko tvrđave, uživajte u zelenim oazama Dunavskog parka i doživite umetnost na potpuno nov način u Muzeju savremene umetnosti. ''Novosadski Kulturni Prolaz'' pruža nezaboravno iskustvo koje istražuje spoj prošlosti, prirode i umetnosti u srcu ovog čarobnog grada.', 3, '{Umetnost,Muzej,Park,Istorija,Kultura}', 0, 169, false, 0, '-infinity', '-infinity', '[]'),
		(-4, -2, 2, 'Šetnja pored Dunava', 'Tura po obali Dunava u Novom Sadu', 2, '{obala,reka}', 1, 100, FALSE, 2.1, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 20, "TransportType": 1}]'),
		(-5, -2, 1, 'Obilazak centra', 'Tura po centru Novog Sada', 2, '{grad,istorija,centar}', 1, 100, FALSE, 0.36, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 30, "TransportType": 1}]'),
		(-6, -2, 1, 'Novosadski Kulturni Prolaz', 'Upustite se u fascinantno putovanje kroz srce Vojvodine sa turom ''Novosadski Kulturni Prolaz''. Ova dinamična ruta vodi vas kroz bogatu istoriju i savremenu vibraciju Novog Sada, obuhvatajući ključne tačke kao što su monumentalna Petrovaradinska tvrđava, mirni Dunavski park i inspirativni Muzej savremene umetnosti Vojvodine. Saznajte ogradu oko tvrđave, uživajte u zelenim oazama Dunavskog parka i doživite umetnost na potpuno nov način u Muzeju savremene umetnosti. ''Novosadski Kulturni Prolaz'' pruža nezaboravno iskustvo koje istražuje spoj prošlosti, prirode i umetnosti u srcu ovog čarobnog grada.', 3, '{Umetnost,Muzej,Park,Istorija,Kultura}', 0, 169, false, 0, '-infinity', '-infinity', '[]'),
		(-7, -2, 1, 'Šetnja pored Dunava', 'Tura po obali Dunava u Novom Sadu', 2, '{obala,reka}', 1, 100, FALSE, 2.1, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 20, "TransportType": 1}]'),
		(-8, -2, 1, 'Obilazak centra', 'Tura po centru Novog Sada', 2, '{grad,istorija,centar}', 1, 100, FALSE, 0.36, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 30, "TransportType": 1}]'),
		(-9, -2, 1, 'Novosadski Kulturni Prolaz', 'Upustite se u fascinantno putovanje kroz srce Vojvodine sa turom ''Novosadski Kulturni Prolaz''. Ova dinamična ruta vodi vas kroz bogatu istoriju i savremenu vibraciju Novog Sada, obuhvatajući ključne tačke kao što su monumentalna Petrovaradinska tvrđava, mirni Dunavski park i inspirativni Muzej savremene umetnosti Vojvodine. Saznajte ogradu oko tvrđave, uživajte u zelenim oazama Dunavskog parka i doživite umetnost na potpuno nov način u Muzeju savremene umetnosti. ''Novosadski Kulturni Prolaz'' pruža nezaboravno iskustvo koje istražuje spoj prošlosti, prirode i umetnosti u srcu ovog čarobnog grada.', 3, '{Umetnost,Muzej,Park,Istorija,Kultura}', 0, 169, false, 0, '-infinity', '-infinity', '[]'),
		(-10, -2, 3, 'Šetnja pored Dunava', 'Tura po obali Dunava u Novom Sadu', 2, '{obala,reka}', 1, 100, FALSE, 2.1, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 20, "TransportType": 1}]'),
		(-11, -2, 3, 'Obilazak centra', 'Tura po centru Novog Sada', 2, '{grad,istorija,centar}', 1, 100, FALSE, 0.36, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 30, "TransportType": 1}]'),
		(-12, -2, 3, 'Novosadski Kulturni Prolaz', 'Upustite se u fascinantno putovanje kroz srce Vojvodine sa turom ''Novosadski Kulturni Prolaz''. Ova dinamična ruta vodi vas kroz bogatu istoriju i savremenu vibraciju Novog Sada, obuhvatajući ključne tačke kao što su monumentalna Petrovaradinska tvrđava, mirni Dunavski park i inspirativni Muzej savremene umetnosti Vojvodine. Saznajte ogradu oko tvrđave, uživajte u zelenim oazama Dunavskog parka i doživite umetnost na potpuno nov način u Muzeju savremene umetnosti. ''Novosadski Kulturni Prolaz'' pruža nezaboravno iskustvo koje istražuje spoj prošlosti, prirode i umetnosti u srcu ovog čarobnog grada.', 3, '{Umetnost,Muzej,Park,Istorija,Kultura}', 0, 169, false, 0, '-infinity', '-infinity', '[]'),
		(-13, -2, 3, 'Šetnja pored Dunava', 'Tura po obali Dunava u Novom Sadu', 2, '{obala,reka}', 1, 100, FALSE, 2.1, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 20, "TransportType": 1}]'),
		(-14, -2, 3, 'Obilazak centra', 'Tura po centru Novog Sada', 2, '{grad,istorija,centar}', 1, 100, FALSE, 0.36, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 30, "TransportType": 1}]'),
		(-15, -2, 0, 'Novosadski Kulturni Prolaz', 'Upustite se u fascinantno putovanje kroz srce Vojvodine sa turom ''Novosadski Kulturni Prolaz''. Ova dinamična ruta vodi vas kroz bogatu istoriju i savremenu vibraciju Novog Sada, obuhvatajući ključne tačke kao što su monumentalna Petrovaradinska tvrđava, mirni Dunavski park i inspirativni Muzej savremene umetnosti Vojvodine. Saznajte ogradu oko tvrđave, uživajte u zelenim oazama Dunavskog parka i doživite umetnost na potpuno nov način u Muzeju savremene umetnosti. ''Novosadski Kulturni Prolaz'' pruža nezaboravno iskustvo koje istražuje spoj prošlosti, prirode i umetnosti u srcu ovog čarobnog grada.', 3, '{Umetnost,Muzej,Park,Istorija,Kultura}', 0, 169, false, 0, '-infinity', '-infinity', '[]'),
		(-16, -2, 0, 'Šetnja pored Dunava', 'Tura po obali Dunava u Novom Sadu', 2, '{obala,reka}', 1, 100, FALSE, 2.1, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 20, "TransportType": 1}]'),
		(-17, -2, 0, 'Obilazak centra', 'Tura po centru Novog Sada', 2, '{grad,istorija,centar}', 1, 100, FALSE, 0.36, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 30, "TransportType": 1}]'),
		(-18, -2, 0, 'Novosadski Kulturni Prolaz', 'Upustite se u fascinantno putovanje kroz srce Vojvodine sa turom ''Novosadski Kulturni Prolaz''. Ova dinamična ruta vodi vas kroz bogatu istoriju i savremenu vibraciju Novog Sada, obuhvatajući ključne tačke kao što su monumentalna Petrovaradinska tvrđava, mirni Dunavski park i inspirativni Muzej savremene umetnosti Vojvodine. Saznajte ogradu oko tvrđave, uživajte u zelenim oazama Dunavskog parka i doživite umetnost na potpuno nov način u Muzeju savremene umetnosti. ''Novosadski Kulturni Prolaz'' pruža nezaboravno iskustvo koje istražuje spoj prošlosti, prirode i umetnosti u srcu ovog čarobnog grada.', 3, '{Umetnost,Muzej,Park,Istorija,Kultura}', 0, 169, false, 0, '-infinity', '-infinity', '[]'),
		(-19, -2, 0, 'Šetnja pored Dunava', 'Tura po obali Dunava u Novom Sadu', 2, '{obala,reka}', 1, 100, FALSE, 2.1, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 20, "TransportType": 1}]'),
		(-20, -2, 0, 'Obilazak centra', 'Tura po centru Novog Sada', 2, '{grad,istorija,centar}', 1, 100, FALSE, 0.36, '2023-12-04 19:04:04.562161+01', '2023-12-04 19:04:04.562161+01', '[{"Duration": 30, "TransportType": 1}]'),
		(-21, -2, 0, 'Novosadski Kulturni Prolaz', 'Upustite se u fascinantno putovanje kroz srce Vojvodine sa turom ''Novosadski Kulturni Prolaz''. Ova dinamična ruta vodi vas kroz bogatu istoriju i savremenu vibraciju Novog Sada, obuhvatajući ključne tačke kao što su monumentalna Petrovaradinska tvrđava, mirni Dunavski park i inspirativni Muzej savremene umetnosti Vojvodine. Saznajte ogradu oko tvrđave, uživajte u zelenim oazama Dunavskog parka i doživite umetnost na potpuno nov način u Muzeju savremene umetnosti. ''Novosadski Kulturni Prolaz'' pruža nezaboravno iskustvo koje istražuje spoj prošlosti, prirode i umetnosti u srcu ovog čarobnog grada.', 3, '{Umetnost,Muzej,Park,Istorija,Kultura}', 0, 169, false, 0, '-infinity', '-infinity', '[]');

-- **************************** REVIEWS ************************************
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-1, 5, 'Great and exciting tour.', 3, '2023-10-15', '2023-10-16', -1, ARRAY['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-2, 1, 'Not interesting at all.', 3, '2023-10-11', '2023-10-15', -2, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=','https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-3, 3, 'OK tour.', 3, '2023-10-10', '2023-10-16', -3, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-4, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -4, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-5, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -5, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-6, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -6, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-7, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -7, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-8, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -8, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-9, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -9, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-10, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -10, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-11, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -11, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-12, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -12, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-13, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -13, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-14, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -14, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-15, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -15, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-16, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -16, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-17, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -17, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-18, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -18, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-19, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -19, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-20, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -20, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);
INSERT INTO tours."Reviews"(
    "Id", "Rating", "Comment", "TouristId", "TourVisitDate", "CommentDate", "TourId", "Images")
VALUES (-21, 5, 'OK tour.', 3, '2023-10-10', '2023-10-16', -21, ARRAY ['https://media.istockphoto.com/id/1160139387/photo/early-morning-in-a-mountains.jpg?s=612x612&w=0&k=20&c=kYe3OeVfR4tx5gQcH3R53QdxwJWV_qSqYFZ7KNRj-Lk=']);

INSERT INTO tours."KeyPoints"(
	"Id", "TourId", "Name", "Description", "Longitude", "Latitude", "LocationAddress", "ImagePath", "Order", "HaveSecret", "Secret", "IsEncounterRequired", "HasEncounter")
	VALUES
		(-1, -1, 'Kej žrtava racije', 'U Novom Sadu, na keju koji danas nosi ime Kej žrtava racije, fašistički okupator je u takozvanoj „januarskoj raciji“ od 21. do 23. januara 1942. izvršio masovno streljanje više od hiljadu nedužnih građana Novog Sada.', 19.855529, 45.252574, 'Сунчани кеј, Нови Сад', 'https://sremskevesti.rs/wp-content/uploads/2023/10/0033-1024x665-1-860x558.jpg', 0, TRUE, '{"Description":"Spomenik je rad vajara Jovana Soldatovića i otkriven je 1971. godine."}', FALSE, FALSE),
		(-2, -1, 'Štrand', 'Štrand je popularna plaža u Novom Sadu. Nalazi se na Dunavu, u blizini Mosta slobode i važi za jednu od najuređenijih plaža na celom toku reke.', 19.847114, 45.236624, 'Сунчани кеј, Нови Сад', 'https://live.staticflickr.com/65535/50079707932_124f7b7949_b.jpg', 1, TRUE, '{"Description":"Ulaz u plažu je besplatan preko godine."}', FALSE, FALSE),
		(-3, -2, 'Trg Svetozara Miletića', 'Svetozar Miletić (Mošorin, 22. februar 1826 — Vršac, 4. februar 1901) bio je advokat, političar i gradonačelnik Novog Sada. Miletić je bio jedan od najznačajnijih i najuticajnijih srpskih političara u Austrougarskoj druge polovine XIX veka.', 19.844811, 45.255049, 'Нови Сад', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 0, TRUE, '{"Description":"Bio je predsednik Družine za ujedinjenje i oslobođenje Srbije sa sedištem na Cetinju, a tada mu je najbliži saradnik na Cetinju Aleksandar Sandić."}', FALSE, FALSE),
		(-4, -2, 'Trg Jovana Jovanovića Zmaja', 'Jovan Jovanović Zmaj (Novi Sad, 6. decembar 1833 — Sremska Kamenica, 14. jun 1904) bio je srpski pesnik, dramski pisac, prevodilac i lekar. Smatra se za jednog od najvećih liričara srpskog romantizma.', 19.847880, 45.256936, 'Змај Јовина 26, Нови Сад', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 1, TRUE, '{"Description":"Njegove najznačajnije zbirke pesama su „Đulići“ i „Đulići uveoci“, prva o srećnom porodičnom životu, a druga o bolu za najmilijima."}', FALSE, FALSE),
		(-5, -3, 'Muzej savremene umetnosti Vojvodine', 'Ovaj muzej je epicentar savremene umetnosti u Novom Sadu. Njegova zbirka obuhvata dela domaćih i međunarodnih umetnika, pružajući posetiocima uvid u širok spektar umetničkih izraza. Prostori muzeja su moderni i inovativni, nudeći iskustvo koje istražuje granice umetnosti i kulture', 19.853335, 45.256402, 'Muzej savremene umetnosti', 'https://sremskevesti.rs/wp-content/uploads/2023/10/0033-1024x665-1-860x558.jpg', 0, true, '{"Images": [""], "Description": ""}', false, false),
		(-6, -3, 'Dunavski park', 'Dunavski park je oaza prirode usred grada, savršeno mesto za opuštanje i šetnju. Sa šarmantnim stazama, cvetnim alejama i jezerom, posetioci mogu uživati u mirnom okruženju. Park takođe često domaćin različitim kulturnim događajima, koncertima i umetničkim izložbama.', 19.850515, 45.25436, 'Dunavski park', 'https://novisad.travel/wp-content/uploads/2019/01/Dunavski-park-_ALE8018_compressed.jpg', 1, true, '{"Images": [""], "Description": ""}', false, false),
		(-7, -3, 'Petrovaradinska tvrđava', 'Petrovaradinska tvrđava je monumentalna tvrđava koja dominira horizontom Novog Sada. Sa svojim impozantnim bastionima, kamenim zidinama i nezaboravnim pogledom na Dunav, tvrđava predstavlja istorijski biser. Posetioci mogu istražiti unikatne podzemne prolaze, uživati u pogledu sa čuvenih Petrovaradinskih satova i doživeti spoj prošlosti i savremene umetnosti tokom EXIT festivala koji se održava unutar tvrđave.', 19.861115, 45.253437, 'Petrovoradinska tvrđava', 'https://live.staticflickr.com/65535/50079707932_124f7b7949_b.jpg', 2, true, '{"Images": [""], "Description": ""}', false, false);

INSERT INTO tours."PublicKeyPoints" ("Id", "Name", "Description", "Longitude", "Latitude", "ImagePath", "Order", "LocationAddress") VALUES (-8, 'Tenk u parkiću', 'Dva srpska tenka jedan uz drugi, veoma blizu, čak i u kontaktu.', 19.853166, 45.256736, 'https://cdn.discordapp.com/attachments/783721881043206154/1187480628950728734/WhatsApp_Image_2023-12-21_at_13.12.05_3d992c74.jpg', 0, 'Park prisajedinjenja');
	
