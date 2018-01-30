START TRANSACTION;
SELECT migrations.set_migration(12);

CREATE TABLE public."AuthTokens"(
	"Token" text NOT NULL,
	"Subject" text NOT NULL,
	"ExpiresAt" timestamp without time zone NULL,
	CONSTRAINT "AuthTokens_pkey" PRIMARY KEY ("Token")
);

COMMIT TRANSACTION;