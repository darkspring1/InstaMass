START TRANSACTION;

SELECT migrations.set_migration(5);

CREATE TABLE public."Accounts"
(
    "Password" character varying(1024) COLLATE pg_catalog."default" NOT NULL,
    "Login" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
	"UserId" UUID NOT NULL,
    CONSTRAINT "Accounts_pkey" PRIMARY KEY ("Login"),
	 CONSTRAINT "FK_Accounts_Users.Id" FOREIGN KEY ("UserId")
	      REFERENCES public."Users" ("Id") MATCH SIMPLE
	      ON UPDATE NO ACTION ON DELETE CASCADE
);

COMMIT TRANSACTION;