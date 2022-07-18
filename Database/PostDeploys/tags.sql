IF NOT EXISTS(SELECT 1 FROM dbo.[tag] WHERE label='Bon plan') INSERT INTO dbo.[tag](label) VALUES('Bon plan')
IF NOT EXISTS(SELECT 1 FROM dbo.[tag] WHERE label='Dernière place') INSERT INTO dbo.[tag](label) VALUES('Dernière place')
IF NOT EXISTS(SELECT 1 FROM dbo.[tag] WHERE label='Complet') INSERT INTO dbo.[tag](label) VALUES('Complet')