## 1. <a name="node"></a> Node.js ir npm
Instaliuojame `nvm` (node version manager)
* Linux/macOS - https://github.com/creationix/nvm#install-script

Instaliuojame `node` su `nvm`
```sh
nvm install --lts
nvm use --lts
```

### 4.1 Įrankiai ir bibliotekos
**eslint** įrankis skirtas kodo formatavimo ir sintaksės klaidų tikrinimui
```sh
npm install acorn --save-dev
npm install eslint --save-dev
npm install eslint-config-airbnb --save-dev
npm install eslint-config-prettier --save-dev
npm install eslint-plugin-immutable --save-dev
npm install eslint-plugin-import --save-dev
npm install eslint-plugin-jsx-a11y --save-dev
npm install eslint-plugin-prettier --save-dev
npm install eslint-plugin-react --save-dev
```

**prettier** Nesirūpinkite kodo stiliumi tuo pasirūpins prettier (kodo formatavimas)
```sh
npm install prettier --save-dev
```

**css-lint** Įrankis kuris padės išvengti klaidų stiliuose. Kartu veikia ir kaip kodo formatavimo hinteris
```sh
npm install csslint --save-dev
npm install stylelint --save-dev
npm install stylelint-config-recommended --save-dev
```

**React browser plugin** Naršyklės dev-tools plėtinys padedantis matyti ir debug’inti vidinę React’o komponentų struktūrą bei jų props’ų reikšmes.
https://chrome.google.com/webstore/detail/react-developer-tools/fmkadmapgofadopljbjfkapdkoienihi

**react** biblioteka, leidžianti kurti komponentus
```sh
npm install react
```

**react-dom** biblioteka, leidžianti renderinti `react` komponentus į DOM medį
```sh
npm install react-dom
```

**parcel** rašysime modernų javascript, kurio sąvybių nepalaiko naršyklės, todėl kodą reiks transpiliuoti ir subundlinti į vieną failą
```
npm install parcel --save-dev
```
