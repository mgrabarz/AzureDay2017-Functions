# AzureDay2017-Functions
Sample implementation of Azure Function exercises.

1. Github webhook trigger. For each commit during push command to this repo, function stores id, user and commit message in Azure Storage Table.
2. Twilio output binding. Request {"To": "+48....", "Message": "...."} to send SMS.
3. Blob Storage Account trigger. Resizes all uloaded images into standarized thumbnail stored in another Blob container.
4. Azure Functions proxy rewrite sample. /api/v1/a is rewritten to /api/HelloWorldAV1, and /api/v1/b to /api/HellowWorldBV1
