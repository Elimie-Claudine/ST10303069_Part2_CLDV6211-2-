USE master   
IF EXISTS (SELECT* FROM sys.databases WHERE name='EventEaseDB')   
DROP DATABASE EventEaseDB 


CREATE DATABASE EventEaseDB
USE EventEaseDB

  

CREATE TABLE Venues ( 
    VenueId INT IDENTITY(1,1) PRIMARY KEY, 
    venueName NVARCHAR(100) NOT NULL, 
    venueLocation NVARCHAR(200),
    Capacity INT, 
    ImageURL VARCHAR(200) 
); 
INSERT INTO Venues (venueName, venueLocation, Capacity, ImageURL) 
    VALUES 
    ('Montecasino', '23 Alexander St, Johannesburg', 300, 'https://ik.imgkit.net/3vlqs5axxjf/external/http://images.ntmllc.com/v4/hotel/T89/T89133/T89133_EXT_ZF36FA.jpg?tr=w-1200%2Cfo-auto'),   
    ('HCM Church', '31 Dukes Ave, Johannesburg', 300, 'https://holycovenantmission.co.za/wp-content/uploads/2023/08/1656-scaled-outside-1024x468.jpeg'  
);   
SELECT * FROM 
Venues 

  


CREATE TABLE Eventss ( 
    EventId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	VenueId INT,
    eventName NVARCHAR(100) NOT NULL, 
    eventDate DATE, 
    eventTime TIME, 
    eventDescription TEXT, 
    ImageURL VARCHAR(200),
    venueName nvarchar(100), 
    FOREIGN KEY (VenueId) REFERENCES Venues(VenueId) 
); 
INSERT INTO Eventss (VenueId, eventName, eventDate, eventTime, eventDescription, ImageURL, venueName)   
     VALUES   
    (1, 'Diary of a Wimpy Kid, The Musical', '2025-05-04', '16:00:00', 'A musical adaptation of the famous book for children.', 'https://i.ytimg.com/vi/0CTZPZnvbb0/maxresdefault.jpg?sqp=-oaymwEmCIAKENAF8quKqQMa8AEB-AH-CYAC0AWKAgwIABABGF4gPCh_MA8=&rs=AOn4CLA64oCDWQCegt2r-nD2lk2QVnVrzg', 'Montecasino'),  
    (2, 'Nathaniel Bassey Live Concert', '2025-04-20', '18:30:00', 'A godly experience just coming for you not to miss !', 'https://i.ytimg.com/vi/-SeMbQize0k/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLBgHoLKI5h_klutD8RmRth2iw8h1A', 'HCM Church')   
;   
SELECT * FROM 
Eventss 
  


CREATE TABLE Bookings ( 
    bookingId INT PRIMARY KEY IDENTITY(1,1), 
    VenueId INT FOREIGN KEY REFERENCES Venues(VenueId),
    EventId INT FOREIGN KEY REFERENCES Eventss(EventId),
    venueName NVARCHAR(100), 
    eventName NVARCHAR(100),
    CustomerEmail VARCHAR(100) NOT NULL, 
    BookingDate DATETIME DEFAULT GETDATE()
);

INSERT INTO Bookings (VenueId, EventId, venueName, eventName, BookingDate, CustomerEmail)
VALUES
(1, 1, 'Montecasino', 'Diary of a Wimpy Kid, The Musical', '2025-05-05 09:25:00', 'epremice@gmail.com'),
(2, 2, 'HCM Church', 'Nathaniel Bassey Live Concert', '2025-03-21 11:21:34', 'epremice04@gmail.com')
;
SELECT * FROM 
Bookings