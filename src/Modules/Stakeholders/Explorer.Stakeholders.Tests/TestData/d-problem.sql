INSERT INTO stakeholders."Problem"(
    "Id", "Category", "Priority", "Description", "ReportedTime", "TouristId", "TourId", "Answer", "Comments", "IsResolved", "IsAnswered", "Deadline")
VALUES (-1, 'Kategorija1','Veoma bitno','Nije ukljuceno u turu.','2023-10-11T11:20:00',-22,-1, NULL, '[]', false, false, 'infinity');

INSERT INTO stakeholders."Problem"(
     "Id", "Category", "Priority", "Description", "ReportedTime", "TouristId", "TourId", "Answer", "Comments", "IsResolved", "IsAnswered", "Deadline")
VALUES (-2, 'Kategorija2', 'Bitno','Smislicu','2023-10-09T10:10:30',-21,-2, NULL, '[]', false, false, 'infinity');

INSERT INTO stakeholders."Problem"(
     "Id", "Category", "Priority", "Description", "ReportedTime", "TouristId", "TourId", "Answer", "Comments", "IsResolved", "IsAnswered", "Deadline")
VALUES (-3, 'Kategorija3', 'Manje bitno','Glupost','2023-10-11T12:19:44',-21,-3, NULL, '[]', false, false, 'infinity');

INSERT INTO stakeholders."Problem"(
     "Id", "Category", "Priority", "Description", "ReportedTime", "TouristId", "TourId", "Answer", "Comments", "IsResolved", "IsAnswered", "Deadline")
VALUES (-4, 'Kategorija3', 'Manje bitno','Glupost','2023-10-11T12:19:44',-21,-3, '{{"Answer": "da","AuthorId": -11}}', '[]', false, true, 'infinity');
INSERT INTO stakeholders."Problem"(
     "Id", "Category", "Priority", "Description", "ReportedTime", "TouristId", "TourId", "Answer", "Comments", "IsResolved", "IsAnswered", "Deadline")
VALUES (-5, 'Kategorija3', 'Manje bitno','Glupost','2023-10-11T12:19:44',-21,-3, '{{"Answer": "da","AuthorId": -11}}', '[{{"Text": "dsadasd","Commenter": {{"Id": 0,"Role": 1,"IsActive": true,"Password": "autor","Username": "autor","ProfilePicture": null}},"CommenterId": 1}}]', false, true, 'infinity');