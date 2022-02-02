Title: TypeAhead Product Survey
Lead: Market research on existing TypeAhead products
Published: 02/01/2022
Tags:
  - TypeAhead
  - Artificial Intelligence
  - Machine Learning
---
### What is TypeAhead?
By Typeahead we are referring to auto complete suggestion products.

A nice high level introduction and general guidelines of predictive search ca be found at
[algolia blog - What is predictive search? What is autocomplete?](https://www.algolia.com/blog/ux/what-are-predictive-search-and-autocomplete)


### System Design Examples
Here are some example of system Design,

- [Pedro Lopes blog - Learning to build an Autocomplete System](https://medium.com/@iftimiealexandru/learning-to-build-an-autocomplete-system-2c2e9f423537) and [his github repository](https://github.com/lopespm/autocomplete) demos an implemention
- https://dingdingsherrywang.medium.com/system-design-autocomplete-62420021adb0
- https://www.educative.io/courses/grokking-the-system-design-interview/mE2XkgGRnmp#div-stylecolorblack-background-colore2f4c7-border-radius5px-padding5px11-personalizationdiv
- From an algorithmic perspective, how to scale TRIE Data Structure
  https://www.youtube.com/watch?v=jFOR1LBEUgM


### Autocomplete APIs
Some existing Autocomplete APIs,

- Bing Autosuggest API, (MS Bing](https://www.microsoft.com/en-us/bing/apis/bing-autosuggest-api)
- Opera autocomplete in custom search engine, ref
    https://superuser.com/questions/606778/opera-autocomplete-in-custome-search-engine
- clearbit Autocomplete API, https://clearbit.com/docs#autocomplete-api
    > Company Autocomplete is an API that lets you auto-complete company names and retreive logo and domain information.


Google TypeAhead team provides a high level overview in their blog posts,
[google blog - How Google autocomplete works in Search](https://blog.google/products/search/how-google-autocomplete-works-search)


> You'll notice we call these autocomplete "predictions" rather than "suggestions" and there's a good reason for that. Autocomplete is designed to help people complete a search they were intending to do, not to suggest new types of searches to be performed. These are our best predictions of the query you were likely to continue entering.


They identifies areas of integrity as well.


google query autocomplete api related references

- https://simplestepscode.com/autocomplete-data-tutorial
- Google Maps placce autocomplete api: https://developers.google.com/maps/documentation/places/web-service/query
- https://stackoverflow.com/questions/6428502/google-search-autocomplete-api
- https://developers.google.com/custom-search/v1/overview
- https://developers.google.com/custom-search/v1/site_restricted_api


Google Cloud Platform Autocomplete Products
- GCP Retail Search has auto complete features. ref, https://cloud.google.com/retail/docs/completion-overview
- GCP Talent Autocomplete: https://cloud.google.com/talent-solution/job-search/docs/autocomplete

On the frontend side, we got following examples,
- https://twitter.github.io/typeahead.js/examples
- Google places autocomplete implementation using Twitter bootstrap typeahead and Google's autocomplete and geocoding services https://gist.github.com/badsyntax/4330899
- https://stackoverflow.com/questions/18385148/typeahead-js-populate-dataset
- https://github.com/walmartlabs/typeahead.js-legacy


### Summary
Twitter's typeahead works as nice example on frontent side: static typeahead prediction.


We can start with a bootstrap and then gradually build a dataset from people's interactions. It is an interesting problem find good quality dataset to cover some category of predictions.


_NOTE_: this article might seem highly technical, let us know in comments if you have any question or have specific questions.