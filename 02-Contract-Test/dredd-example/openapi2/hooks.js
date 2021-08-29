const hooks = require('hooks');
const {MongoClient} = require('mongodb');


let client;
let db;


// Fixtures
const gist = {
  '_id': '42',
  'created_at': new Date().toISOString(),
  'description': 'Gist description...',
  'content': 'String content...',
}

const star = {
  'gist_id': '42',
  'starred': true,
}


// Setup database connection before Dredd starts testing
hooks.beforeAll((transactions, done) => {
  const mongoURI = process.env.MONGO_URI || 'mongodb://localhost';
  MongoClient.connect(mongoURI, { useNewUrlParser: true }, function (err, c) {
    if (!err) {
      client = c;
      db = c.db('dredd-example');
    }
    done(err);
  });
});

// Close database connection after Dredd finishes testing
hooks.afterAll((transactions, done) => {
  client.close();
  done();
});


// Before each test let's fix the media type - unfortunately current support
// of OpenAPI2 in Dredd works only for application/json as of now
hooks.beforeEach((transaction, done) => {
  let contentType;

  if (transaction.expected.statusCode == '204') {
    transaction.request.headers['Accept'] = 'text/plain';
    delete transaction.expected.headers['Content-Type'];
  } else {
    transaction.request.headers['Accept'] = 'application/hal+json';
    transaction.expected.headers['Content-Type'] = 'application/hal+json';
  }
  done();
});

// After each test clear contents of the database (we want isolated tests)
hooks.afterEach((transaction, done) => {
  db.collection('gists').deleteMany({}, (err) => {
    if (err) { return done(err) }

    db.collection('stars').deleteMany({}, (err) => {
      done(err);
    });
  });
});


// To test work with Gists and Stars in isolation, we need to add some prior
// to certain HTTP transactions Dredd is about to make
hooks.before('/gists/{id} > GET > 200 > application/json', (transaction, done) => {
  db.collection('gists').insertOne(gist, (err) => {
    done(err);
  });
});

hooks.before('/gists/{id} > PATCH > 200 > application/json', (transaction, done) => {
  db.collection('gists').insertOne(gist, (err) => {
    done(err);
  });
});

hooks.before('/gists > GET > 200 > application/json', (transaction, done) => {
  db.collection('gists').insertOne(gist, (err) => {
    done(err);
  });
});

hooks.before('/gists/{id}/star > GET > 200 > application/json', (transaction, done) => {
  db.collection('stars').insertOne(star, (err) => {
    done(err);
  });
});
