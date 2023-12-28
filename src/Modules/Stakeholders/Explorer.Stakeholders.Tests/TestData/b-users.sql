--pass: admin
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "ProfilePicture", "IsActive")
VALUES (-10, 'admin', '$2a$12$OH46cBmgFA8dYja017kRluqsORzigbs/9JEU7sdjj76WalFpgyELe', 0, '', true);
--pass: autor1
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "ProfilePicture", "IsActive")
VALUES (-11, 'autor1', '$2a$12$yPz7fMJ9zBG58NTJfj15MOnqz7O5phWbnKu41/ZybBuB2hX.5LnkC', 1, '', true);
--pass: autor2
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "ProfilePicture", "IsActive")
VALUES (-12, 'autor2', '$2a$12$hOVi7944tKuRFllbJDnD6eHIMXGASg7MGCXWwzD67rpBuDF0voy5K', 1, '',true);
--pass:autor3
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "ProfilePicture", "IsActive")
VALUES (-13, 'autor3', '$2a$12$vwqk.tbUBkSDOqnQIrtOQ.uKKwNmB0hKbP0CH5CP5acKIZg2ourKK', 1, '', true);
--pass: turista1
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "ProfilePicture", "IsActive")
VALUES (-21, 'turista1', '$2a$12$DTul5c2LeHymADi7lsFjQu5mrIS5XFOVQmXQPjnTF7Z3iQ5rGlyEC', 2, '', true);
--pass:turista2
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "ProfilePicture", "IsActive")
VALUES (-22, 'turista2', '$2a$12$tD8MsEMsNjwqhzHkkVpzw.irBFdxK18BjVAf5CUy210QQuZXfH8dS', 2, '', true);
--pass: turista3
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "ProfilePicture", "IsActive")
VALUES (-23, 'turista3', '$2a$12$jNeUxFkz23aGLrFP0Cl1Mu8KyKe2DZt6q0BroSJR.utAkpM8zvcsO', 2, '', true);