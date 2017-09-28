CREATE DATABASE IF NOT EXISTS RevitPCF
	CHARACTER SET = 'utf8'
	COLLATE = 'utf8_general_ci';

CREATE TABLE IF NOT EXISTS RevitPCF.Projects 
(
	Identification bigint NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Title varchar(100) NOT NULL,
	ExportPath varchar(260) NOT NULL,
	Date TIMESTAMP,
	Description TEXT
);

CREATE TABLE IF NOT EXISTS RevitPCF.Revisions
(
	Project bigint NOT NULL,
	CONSTRAINT 'FK_Revisions_Project'
		FOREIGN KEY (Project) REFERENCES RevitPCF.Projects (Identification)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	Date TIMESTAMP,
	RevisionNumber int NOT NULL,
	Description TEXT,
	CONSTRAINT 'PK_Revisions'
		PRIMARY KEY (Project, RevisionNumber)
);

CREATE TABLE IF NOT EXISTS RevitPCF.Lines
(
	Project bigint NOT NULL,
	CONSTRAINT 'FK_Lines_Project'
		FOREIGN KEY (Project) REFERENCES RevitPCF.Projects (Identification)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	Name varchar(100) NOT NULL,
	Description TEXT,
	CONSTRAINT 'PK_Lines'
		PRIMARY KEY (Project, Name)
);

CREATE TABLE IF NOT EXISTS RevitPCF.Materials
(
	ItemCode varchar(100) NOT NULL PRIMARY KEY,
	Description TEXT,
);

CREATE TABLE IF NOT EXISTS RevitPCF.Components
(
	Project bigint NOT NULL,
	CONSTRAINT 'FK_Components_Project'
		FOREIGN KEY (Project) REFERENCES RevitPCF.Projects (Identification)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	FamilyType varchar(100) NOT NULL,
	CONSTRAINT 'PK_Components'
		PRIMARY KEY (Project, FamilyType),
	PCFType varchar(30) NOT NULL,
	SKEY varchar(30),
	ItemCode varchar(100) NOT NULL,
	CONSTRAINT 'FK_Components_Material'
		FOREIGN KEY (ItemCode) REFERENCES RevitPCF.Materials (ItemCode)
		ON DELETE SET NULL
		ON UPDATE CASCADE,
	Category ENUM('FABRICATION', 'ERECTION', 'OFFSHORE') NOT NULL
);

CREATE TABLE IF NOT EXISTS RevitPCF.Instances
(
	UCI varchar(50) NOT NULL PRIMARY KEY,
	--Project와 Family Type을 활용하여, 해당 Component를 찾는다.
	Project bigint NOT NULL,
	CONSTRAINT 'FK_Instances_Project'
		FOREIGN KEY (Project) REFERENCES RevitPCF.Projects (Identification)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	FamilyType varchar(100) NOT NULL,
	CONSTRAINT 'FK_Instances_Component'
		FOREIGN KEY (FamilyType) REFERENCES RevitPCF.Components (FamilyType)
		ON DELETE RESTRICT
		ON UPDATE CASCADE,
	Line varchar(100),
	CONSTRAINT 'FK_Instances_Line'
		FOREIGN KEY (Line) REFERENCES RevitPCF.Lines (Name)
		ON DELETE SET NULL
		ON UPDATE CASCADE,
	--좌표를 저장하는 방법을 찾아보자!
	--PrimaryEndPoint varchar(30),
	PrimaryEndType varchar(30),
	--SecondaryEndPoint varchar(30),
	SecondaryEndType varchar(30),
	--BranchEndPoint varchar(30),
	BranchEndType varchar(30),
);