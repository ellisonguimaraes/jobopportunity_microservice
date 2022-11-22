CREATE DATABASE JobDb;
USE JobDb;

CREATE TABLE JobAdvertisement(
	id VARCHAR(36) NOT NULL,
	title VARCHAR(150) NOT NULL,
	company VARCHAR(80) NOT NULL,
	description TEXT NOT NULL,
	modality TINYINT NOT NULL,
	benefit TEXT,
	min_payrange DECIMAL,
	max_payrange DECIMAL,
	requerements TEXT NOT NULL,
	monthly_hours INT NOT NULL,
	email VARCHAR(100) NOT NULL,
	phone_number VARCHAR(45) NOT NULL,
	link VARCHAR(300),
	date_limit DATETIME2 NOT NULL,
	is_active BIT NOT NULL,
	user_id VARCHAR(36) NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT job_advertisement_pk PRIMARY KEY (id)
)

CREATE TABLE Address(
	id VARCHAR(36) NOT NULL,
	street VARCHAR(45),
	district VARCHAR(45),
	city VARCHAR(45) NOT NULL,
	state VARCHAR(45) NOT NULL,
	country VARCHAR(45) NOT NULL,
	job_advertisement_id VARCHAR(36) NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT address_pk PRIMARY KEY (id)
)

CREATE TABLE Category(
	id VARCHAR(36) NOT NULL,
	name VARCHAR(45) NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT category_pk PRIMARY KEY (id)
)

CREATE TABLE CategoryJob(
	id VARCHAR(36) NOT NULL,
	category_id VARCHAR(36) NOT NULL,
	job_advertisement_id VARCHAR(36) NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT category_job_pk PRIMARY KEY (id)
)

ALTER TABLE Address
ADD CONSTRAINT fK_address_jobAdvertisement
FOREIGN KEY (job_advertisement_id) REFERENCES JobAdvertisement(id);

ALTER TABLE CategoryJob
ADD CONSTRAINT fk_categoryjob_jobadvertisement
FOREIGN KEY (job_advertisement_id) REFERENCES JobAdvertisement(id);

ALTER TABLE CategoryJob
ADD CONSTRAINT fk_categoryjob_category
FOREIGN KEY (category_id) REFERENCES Category(id);

ALTER TABLE Address
ADD CONSTRAINT uc_address_job_advertisement_id UNIQUE (job_advertisement_id);

ALTER TABLE CategoryJob
ADD CONSTRAINT uc_categoryjob_categoryid_advertisementid UNIQUE (category_id, job_advertisement_id);

DELETE FROM Category;
INSERT INTO Category (id, name, created_at, updated_at) VALUES 
('dbe1c83e-8764-43ad-b0e4-bbf080a35f0e', 'industria', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('2e0dbd9a-f876-4bf4-ba35-0afbd48599d0', 'engenharia', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('97ffaa01-2d0e-4e2c-b092-83a0efb09698', 'saude', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('ddefa7b7-9838-483e-9f67-72d055889269', 'tecnologia', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('e6824c45-b72f-445d-8f90-3152b5a6626c', 'humanas', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5));

DELETE FROM JobAdvertisement;
INSERT INTO JobAdvertisement (id, title, company, description, modality, benefit, min_payrange, max_payrange, requerements, monthly_hours, email, phone_number, link, date_limit, is_active, created_at, updated_at, user_id) VALUES
('8faf8c21-bd2a-4b0e-8145-7dd1e1462577', 'Desenvolvedor C#', 'Take Blip', 'Venha trabalhar na empresa YYY', 1, 'Plano de saúde e plano dentário', 7000.0, 12.500, 'EF, Sql Server, Identity, CQRS', 200, 'ellison.guimaraes@gmail.com', '11912344321', 'https://empresayyy.com', convert(datetime2,'18-06-12 10:34:09 PM',5), 1, convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5), cast(NEWID() as VARCHAR(36))),
('d1a69a90-76a5-412a-ab0e-5b0cfa46e14a', 'Desenvolvedor PHP', 'Radix', 'Venha trabalhar na empresa ZZZ', 0, 'Plano de saúde e plano dentário', 16000.0, 24.500, 'Lavar e secar', 220, 'programabc@gmail.com', '11912344321', 'https://empresazzz.com', convert(datetime2,'18-06-12 10:34:09 PM',5), 1, convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5), cast(NEWID() as VARCHAR(36))),
('e837f6e9-6e6f-4d18-9f30-1fefbcb981d1', 'Engenheiro elétrico', 'Construtora Mattos', 'Venha trabalhar na empresa XXX', 2, 'Plano de saúde e plano dentário', 5000.0, 8.500, 'Trabalhou com prédios', 100, 'engenheiroabc@gmail.com', '11912344321', 'https://empresaxxx.com', convert(datetime2,'18-06-12 10:34:09 PM',5), 1, convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5), cast(NEWID() as VARCHAR(36)));

DELETE FROM CategoryJob;
INSERT INTO CategoryJob (id, category_id, job_advertisement_id, created_at, updated_at) VALUES 
('2464a379-f44c-402b-bf7b-9fea4ce0d513', 'dbe1c83e-8764-43ad-b0e4-bbf080a35f0e', 'e837f6e9-6e6f-4d18-9f30-1fefbcb981d1', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('7a59c55b-16ff-4183-a69c-faf539088471', '2e0dbd9a-f876-4bf4-ba35-0afbd48599d0', 'e837f6e9-6e6f-4d18-9f30-1fefbcb981d1', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('6d936387-1999-4690-acd5-3ecf428200b6', 'ddefa7b7-9838-483e-9f67-72d055889269', '8faf8c21-bd2a-4b0e-8145-7dd1e1462577', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('bb836268-9f8f-40f9-8397-2334edb9d2f4', 'ddefa7b7-9838-483e-9f67-72d055889269', 'd1a69a90-76a5-412a-ab0e-5b0cfa46e14a', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('0162eb5c-7d32-44b9-95fa-743fc7288249', 'dbe1c83e-8764-43ad-b0e4-bbf080a35f0e', 'd1a69a90-76a5-412a-ab0e-5b0cfa46e14a', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5));

DELETE FROM Address;
INSERT INTO Address (id, street, district, city, state, country, created_at, updated_at, job_advertisement_id) VALUES
('1fb6ae69-51f0-4005-9635-5d00261e18a6', 'Rua Epinal', 'Conceicao', 'Itabuna', 'Bahia', 'Brasil', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5), 'e837f6e9-6e6f-4d18-9f30-1fefbcb981d1'),
('3c82455b-8053-4125-b981-e478b6645598', 'Street X', 'District X', 'Orlando', 'Florida', 'Estados Unidos', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5), 'd1a69a90-76a5-412a-ab0e-5b0cfa46e14a'),
('eb4b5f66-3ade-4654-b670-b5945ff90959', 'Rua F', 'Centro', 'Itabuna', 'Bahia', 'Brasil', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5), '8faf8c21-bd2a-4b0e-8145-7dd1e1462577');