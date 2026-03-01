// Post-response script for visualizing events as a table
var template = `
<style>
    .events-table {
        width: 100%;
        border-collapse: collapse;
        font-family: Arial, sans-serif;
        font-size: 14px;
    }
    .events-table th {
        background-color: #FF6C37;
        color: white;
        padding: 12px 8px;
        text-align: left;
        border: 1px solid #ddd;
    }
    .events-table td {
        padding: 10px 8px;
        border: 1px solid #ddd;
    }
    .events-table tr:nth-child(even) {
        background-color: #f9f9f9;
    }
    .events-table tr:hover {
        background-color: #f1f1f1;
    }
    .status-badge {
        padding: 4px 8px;
        border-radius: 4px;
        font-size: 12px;
        font-weight: bold;
    }
    .status-not-started {
        background-color: #ffeeba;
        color: #856404;
    }
    h2 {
        color: #333;
        font-family: Arial, sans-serif;
    }
</style>

<h2>Soccer Events - {{date}}</h2>
<table class="events-table">
    <thead>
        <tr>
            <th>Event</th>
            <th>Home Team</th>
            <th>Away Team</th>
            <th>League</th>
            <th>Venue</th>
            <th>Time</th>
            <th>Status</th>
        </tr>
    </thead>
    <tbody>
        {{#each events}}
        <tr>
            <td>{{strEvent}}</td>
            <td>{{strHomeTeam}}</td>
            <td>{{strAwayTeam}}</td>
            <td>{{strLeague}}</td>
            <td>{{strVenue}}</td>
            <td>{{strTime}}</td>
            <td><span class="status-badge status-not-started">{{strStatus}}</span></td>
        </tr>
        {{/each}}
    </tbody>
</table>
<p><strong>Total Events:</strong> {{totalEvents}}</p>
`;

function constructVisualizerPayload() {
    var response = pm.response.json();
    var events = response.events || [];
    var date = events.length > 0 ? events[0].dateEvent : "N/A";
    
    return {
        events: events,
        date: date,
        totalEvents: events.length
    };
}

pm.visualizer.set(template, constructVisualizerPayload());
