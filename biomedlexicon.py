from bs4 import BeautifulSoup
import requests

def search_biomedlexicon(search_term):
    
    request_data = {
        "-V": "medterms",
        "-d": "results.html",
        "_TSEARCHTEXT": search_term,
    }

    response = requests.post("http://www.biomedlexicon.com/cgibin/hweb", data=request_data)
    
    response_soup = BeautifulSoup(response.content, 'lxml')

    response_table = response_soup.select_one('.slgray')

    if response_table is None:
        return {'error':'Nothing was found'}
    else:
        table_rows = response_table.find_all('tr')
        return {'translation' : table_rows[1].find('td').get_text()}