connect storedb
CREATE TABLE product
(
 id serial PRIMARY KEY,
 title VARCHAR (50) NOT NULL,
 description VARCHAR (100) NOT NULL,
 price INT NOT NULL
);
ALTER TABLE "product" OWNER TO storedb;
Insert into product(title,description,price) values( 'Papas Margarita de Limón','Paquete de papas de la marca margarita',1500);
Insert into product(title,description,price) values( 'Ponqué Gala','Description 2',1700);
Insert into product(title,description,price) values( 'Papas Margarita de Pollo','Description 3',1500);
Insert into product(title,description,price) values( 'DeTodito BBQ','Description 4',2000);