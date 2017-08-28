START TRANSACTION;

SELECT migrations.set_migration(4);

DROP TABLE public."RefreshTokens";

CREATE TABLE public."RefreshTokens"
(
    "Id" character varying(4096) NOT NULL,
	"ApplicationId" character varying NOT NULL,
	"ProtectedTicket" text NOT NULL,
    CONSTRAINT "RefreshTokens_pkey" PRIMARY KEY ("Id"),
 
	CONSTRAINT "FK_RefreshTokens_Applications.Id" FOREIGN KEY ("ApplicationId")
        REFERENCES public."Applications" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
);

COMMIT TRANSACTION;