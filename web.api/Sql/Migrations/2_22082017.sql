START TRANSACTION;

SELECT migrations.set_migration(2);

ALTER TABLE public."Users" ADD Unique("Email");

COMMIT TRANSACTION;