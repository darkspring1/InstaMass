START TRANSACTION;

SELECT migrations.set_migration(3);

CREATE TABLE public."RefreshTokens"
(
    "Id" character varying(1024) NOT NULL,
	"UserId" uuid NOT NULL,
	"ApplicationId" character varying NOT NULL,
	"IssuedAt" timestamp without time zone NOT NULL,
	"ExpiresdAt" timestamp without time zone NOT NULL,
	"ProtectedTicket" character varying(4096) NOT NULL,
    CONSTRAINT "RefreshTokens_pkey" PRIMARY KEY ("Id"),
 
	CONSTRAINT "FK_RefreshTokens_Applications.Id" FOREIGN KEY ("ApplicationId")
        REFERENCES public."Applications" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
		
    CONSTRAINT "FK_RefreshTokens_Users.Id" FOREIGN KEY ("UserId")
        REFERENCES public."Users" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
);

COMMIT TRANSACTION;