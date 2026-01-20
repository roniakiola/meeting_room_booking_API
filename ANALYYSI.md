1. Mitä tekoäly teki hyvin?
- Tekoäly osasi nopeasti tuottaa toimivan perusratkaisun, joka täytti tehtävänannon keskeiset vaatimukset ilman suurempia ylimääräisiä ominaisuuksia. Toimintalogiikka vaatimuksille oli alusta alkaen toimivaa, mutta ehkä kokeneemmän seniorin silmin näissä olisi optimoinille varaa.
- Alussa tehtyjen ohjeiden jälkeen tekoäly ymmärsi pyydettäessä kysyä tarkentavia ohjeita toteutusta varten, mm. lisäkysymys päällekkäisten varausten sallitusta logiikasta oli mielestäni oivallinen.
- Piti projektin struktuurin ja teknisen ratkaisun suhteellisen yksinkertaisena. Koodi on helposti luettavaa ja arkkitehtuuri ymmärrettävissä nopeallakin silmäyksellä.
- Etenkin arkkitehtuurin näkökulmasta tekoäly vältti "over engineeringin", ehkä tähän vaikutti se, että sitä pyydettiin eksplisiittisesti pitäytymään yksinkertaisuudessa.

2. Mitä tekoäly teki huonosti?
- Kaikki virhetilanteet "heitettiin" aluksi InvalidOperationException -poikkeuksina, vaikka osa virhetilanteista liittyi virheellisiin käyttäjäsyötteisiin ja osa järjestelmän/varausten tilaan ("päällekkäinen varaus", johon kyllä InvalidOperationException mielestäni olikin oikea valinta).
- BookingServicen interface oli aluksi samassa tiedostossa kuin implementaatiokoodi, joten eriytin IBookingServicen omaan tiedostoonsa. Tässä tekoäly ehkä oikaisi yksinkertaisuuden nimissä, mutta modulaarisuus on silti huomioitava.
- Vaikka pahimmalta rönsyilyltä vältyttiin, niin tekoäly pyytämättä implementoi lokituksen (tavallaan ihan hyvä, mutta näin pienessä projektissa tarpeeton), sekä tuotti ennen aikaisesti selitteet OpenAPI/Swaggeria varten.
- Tekoäly ei trigannut puuttuvaan .gitignoreen projektin juuressa, joka oli mielestäni hiukan erikoista, vaikka tässä projektissa ei mitään sensitiviistä dataa tai avaimia ole käytössä. Tämä on kuitenkin yksi ensimmäisiä asioita joita itse katson projektin alustuksessa kuntoon varmuuden vuoksi, joten se ihmetytti.

3. Mitkä olivat tärkeimmät parannukset, jotka teit tekoälyn tuottamaan koodiin ja miksi?
- Virheenkäsittelyn semantiikan refaktorointi paransi koodin ilmaisukykyä ja loi selkeämmän pohjan HTTP-virheiden käsittelyyn rajapinnassa.
- GET /api/bookings?room={roomId} endpointtiin lisäsin huoneen validaation, jolloin virheellisellä huone-ID:llä ei palauteta tyhjää taulukkoa, vaan HTTP 404. Periaatteessa tyhjän taulun palauttaminen ei ole väärin, mutta tämä lisää mielestäni rajapinnan käytettävyyttä.
- IBookingServicen eriyttäminen omaan tiedostoonsa. Periaatteessa tämän projektin skaala huomioon ottaen ei suuri muutos, mutta isommassa koodikannassa merkittävä 