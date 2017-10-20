START TRANSACTION;
SELECT migrations.set_migration(9);
ALTER TABLE public."Accounts" ADD COLUMN  "Version" INT NOT NULL DEFAULT 1;
ALTER TABLE public."Accounts" ADD COLUMN  "ExternalSystemVersion" INT NOT NULL DEFAULT 1;

ALTER TABLE public."Tasks" ADD COLUMN  "Version" INT NOT NULL DEFAULT 1;
ALTER TABLE public."Tasks" ADD COLUMN  "ExternalSystemVersion" INT NOT NULL DEFAULT 1;

ALTER TABLE public."Accounts" ALTER COLUMN  "Version" DROP DEFAULT;
ALTER TABLE public."Accounts" ALTER COLUMN  "ExternalSystemVersion" DROP DEFAULT;

ALTER TABLE public."Tasks" ALTER COLUMN  "Version" DROP DEFAULT;
ALTER TABLE public."Tasks" ALTER COLUMN  "ExternalSystemVersion" DROP DEFAULT;

COMMIT TRANSACTION;