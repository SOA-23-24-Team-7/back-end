INSERT INTO stakeholders."Users"("Id", "Username", "Password", "Role", "ProfilePicture", "IsActive")
VALUES
    (-1, 'dop', 'dop123', 0, '', true),
    (-2, 'author', 'author', 1, 'https://imgs.search.brave.com/n8Gm53DmrCXkPu9d7FpTq1FLO8Nj00zDwzTlFac8HH4/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/cHJlbWl1bS1waG90/by9oYW5kc29tZS10/b3VyaXN0LW1hbi1s/b29rLW1hcC13aGls/ZS1wb2ludGluZy1m/aW5nZXItZGlyZWN0/aW9uLWRlc3RpbmF0/aW9uLXRyYXZlbC1j/b25jZXBfNTY4NTQt/Mzk4NS5qcGc_c2l6/ZT02MjYmZXh0PWpw/Zw', true),
    (-3, 'tourist', 'tourist', 2, 'https://imgs.search.brave.com/_8gIhJxRAq9aqREpTnh_wGcNfv4JwgEssYFmzKDWow8/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5nZXR0eWltYWdl/cy5jb20vaWQvMTMw/MTAwMzg3NC9waG90/by95b3VuZy13b21h/bi1hcnJpdmluZy1h/dC1hLXRyb3BpY2Fs/LXJlc29ydC1mb3It/aGVyLXZhY2F0aW9u/LmpwZz9zPTYxMng2/MTImdz0wJms9MjAm/Yz1BT3N1eWltalZm/QkducTZZeVVhOVZ0/aXRQWE9rdWJOMDFq/TFp0R19lb1ZjPQ', true);

INSERT INTO stakeholders."People"("Id", "UserId", "Name", "Surname", "Email", "Bio", "Motto")
VALUES
    (-1, -2, 'John', 'Johnson', 'author@gmail.com', 'I love making tours.', 'Never give up.'),
    (-2, -3, 'Charles', 'Smith', 'tourist@gmail.com', 'I love tours.', 'Stay strong.');
