import React, { useState } from 'react';
import './App.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';

function App() {
  const [inputText, setInputText] = useState<string>("");
  const [formData, setFormData] = useState({
    searchTextField: "",
  });
  const [searchText, setSearchText] = useState<string>("");
  const [searchResults, setSearchResults] = useState<any>([]);
  const subscriptionKey = 'J9EuTlAb4zDXSDbG8tKJ6YckgZHqeMOzGykTNaHKFwBDo4sWO0cO1CJRKlWw';
  const searchUrl = 'https://api.serphouse.com/serp/live';

  let inputHandler = (e: any) => {
    setFormData({ searchTextField: '' })
    setSearchText(e.target.value.toLowerCase());

  };

  const handlePreview = () => {
    handleSearch(searchText)
  }

  const handleSearch = async (query: any) => {
    try {
      let data = {
        "q": query,
        "domain": "google.com",
        "loc": "Abernathy,Texas,United States",
        "lang": "en",
        "device": "desktop",
        "serp_type": "web",
        "page": "1",
        "verbatim": "0"
      }

      const headers = {
        "accept": "application/json",
        "Content-Type": "application/json",
        "Authorization": `Bearer ${subscriptionKey}`
      }
      const response = await axios.post(searchUrl, {
        data: data
      }, { headers: headers });
      setSearchResults(response.data.results.results.organic);
      // setInputText(response.data.results.results.organic);
    } catch (error) {
      setFormData({ searchTextField: 'Search field is required' })
      // console.error('Error fetching search results:', error);
    }
  };

  return (
    <div className='container'>
      <h3 className='text-center mt-4'>Search Anything</h3>
      <div className="mt-4">
        <div className="input-group">
          <div className="form-outline input-search">
            <input type="search" id="form1" className="form-control"
              onChange={inputHandler}
            />
          </div>
          <button id="search-button" type="submit" className="btn btn-primary" onClick={handlePreview}>
            <FontAwesomeIcon icon={faSearch} />
          </button>
        </div>
        {formData.searchTextField && <div className='error-field'> {formData.searchTextField}</div>}
        <div>

          {searchResults.map((result: any) => (
            <div className='mt-4 search-result-data'>
              <div className='p-4'>
                <h4>
                  {result.title}
                </h4>
                <a href={result.link}>
                  {result.displayed_link}
                </a>
                <p>
                  {result.snippet}
                </p>
                <br />
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default App;
