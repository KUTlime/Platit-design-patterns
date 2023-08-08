var sidlo = new TweetAuthor("Jindřich Šídlo");
var fan1 = new Fan("Radek");
var fan2 = new Fan("Milan");

var unfan1 = sidlo.Subscribe(fan1);
var unfa2 = sidlo.Subscribe(fan2);

sidlo.Tweet("I don't like Pitomio.");

unfan1.Dispose();

sidlo.Tweet("I don't like Miloš Zeman.");
unfa2.Dispose();

sidlo.Tweet("I like fooball.");
