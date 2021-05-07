from bs4 import BeautifulSoup
import requests

headers = {
    "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:69.0),john@doe.com",
    "Referer": "https://www.google.com/",
}
url = "https://www.mayoclinic.org/search/search-results?q=hiv"

response = requests.get(url, headers=headers)

response_soup = BeautifulSoup(response.content, "lxml")

links = response_soup.select_one(".navlist")

link = links.find('a', href = True)

page = requests.get(link['href'] + '?p=1', headers=headers)

print(page.content)