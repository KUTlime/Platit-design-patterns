var valiant = new DefiantShipClass();
_ = valiant.Strategy.GetDamage();

var enterprise = new GalaxyShipClass();
enterprise.Strategy.GetSpeed();

enterprise.Strategy = new EscapeStrategy();
enterprise.Strategy.GetSpeed();
