import http from 'k6/http';
import { check, sleep } from 'k6';

// export const options = {
//   // A number specifying the number of VUs to run concurrently.
//   vus: 10,
//   // A string specifying the total duration of the test run.
//   duration: '30s',

//   // The following section contains configuration options for execution of this
//   // test script in Grafana Cloud.
//   //
//   // See https://grafana.com/docs/grafana-cloud/k6/get-started/run-cloud-tests-from-the-cli/
//   // to learn about authoring and running k6 test scripts in Grafana k6 Cloud.
//   //
//   // ext: {
//   //   loadimpact: {
//   //     // The ID of the project to which the test is assigned in the k6 Cloud UI.
//   //     // By default tests are executed in default project.
//   //     projectID: "",
//   //     // The name of the test in the k6 Cloud UI.
//   //     // Test runs with the same name will be grouped.
//   //     name: "script.js"
//   //   }
//   // },

//   // Uncomment this section to enable the use of Browser API in your tests.
//   //
//   // See https://grafana.com/docs/k6/latest/using-k6-browser/running-browser-tests/ to learn more
//   // about using Browser API in your test scripts.
//   //
//   // scenarios: {
//   //   // The scenario name appears in the result summary, tags, and so on.
//   //   // You can give the scenario any name, as long as each name in the script is unique.
//   //   ui: {
//   //     // Executor is a mandatory parameter for browser-based tests.
//   //     // Shared iterations in this case tells k6 to reuse VUs to execute iterations.
//   //     //
//   //     // See https://grafana.com/docs/k6/latest/using-k6/scenarios/executors/ for other executor types.
//   //     executor: 'shared-iterations',
//   //     options: {
//   //       browser: {
//   //         // This is a mandatory parameter that instructs k6 to launch and
//   //         // connect to a chromium-based browser, and use it to run UI-based
//   //         // tests.
//   //         type: 'chromium',
//   //       },
//   //     },
//   //   },
//   // }
// };

export const options = {
  insecureSkipTLSVerify: true,
  noConnectionReuse: false,
  stages: [
      // A list of virtual users { target: ..., duration: ... } objects that specify 
      // the target number of VUs to ramp up or down to for a specific period.
      { duration: '5m', target: 100 }, // simulate ramp-up of traffic from 1 to 100 users over 5 minutes.
      { duration: '10m', target: 100 }, // stay at 100 users for 10 minutes
      { duration: '5m', target: 0 }, // ramp-down to 0 users
  ],
  thresholds: {
      // A collection of threshold specifications to configure under what condition(s) 
      // a test is considered successful or not
      'http_req_duration': ['p(99)<1500'], // 99% of requests must complete below 1.5s
  }
};

// The function that defines VU logic.
//
// See https://grafana.com/docs/k6/latest/examples/get-started-with-k6/ to learn more
// about authoring k6 scripts.
//
// export default function() {
//   http.get('https://test.k6.io');
//   sleep(1);
// }

export default function () {
  // Here, we set the endpoint to test.
  const response = http.get('http://localhost:3000/gateway/user');

  // An assertion
  check(response, {
      'is status 200': (x) => x.status === 200
  });

  sleep(1);
}