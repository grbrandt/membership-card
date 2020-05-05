## Relational model diagram
![Database diagram](https://github.com/grbrandt/membership-card/blob/master/Documentation/Images/Database%20diagram.png)
#### About deleting entities
Some entities are implements the [ISoftDeletable](https://github.com/grbrandt/membership-card/blob/master/Membership.Data/Entities/ISoftDeleteable.cs) interface. These entities will not be removed physically from the database when the a delete operation is executed against a record through the data context. Instead, it will set the *IsDeleted* column to **True**. 

### Clubs entity
The clubs entity contains basic information about a registered club. A club can have many members which are linked through the **Membership**-entity
PK|Column name|Type|Nullable|Default Value|Notes
--|-----------|----|--------|-------------|-----
Y|Id|int|False
||Name|nvarchar(max)|False
||QRDefaultValidityPeriod|time|False|7 days|Default validity period of a generated QR code in days
||IsDeleted|bit|False

### Members entity
The member entity contains basic information about a registered club. A club can have many members which are linked through the **Membership**-entity
PK|Column name|Type|Nullable|Default Value|Notes
--|-----------|----|--------|-------------|-----
Y|Id|int|False
||Name|nvarchar(450)|False
||Email|nvarchar(max)|False|
||IsDeleted|bit|False


### Membership entity
The membership entity creates a many-to-many relation between clubs and members. That is, a person can be a member in several clubs, and a club can have several members. 
PK|Column name|Type|Nullable|Default Value|Notes
--|-----------|----|--------|-------------|-----
Y|ClubId|int|False
Y|MemberId|int|False
||MemberNumber|nvarchar(max)|False|
