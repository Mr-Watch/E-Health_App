from bs4 import BeautifulSoup
import requests

headers = {
    "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:69.0),john@doe.com",
    "Referer": "https://www.google.com/",
}
url = "https://www.mayoclinic.org/search/search-results?q=asthma"

response = requests.get(url, headers=headers)

response_soup = BeautifulSoup(response.content, "lxml")

links = response_soup.select_one(".navlist").find_all('a', href=True)
url = ''
for link in links:
    if link.get_text().find('- Symptoms and causes -') != -1:
        url = link
        break

response = requests.get(url['href'], headers=headers)

response_soup = BeautifulSoup(response.content, "lxml")

links = response_soup.select_one(
    '#access-nav > ul:nth-child(1)').find_all('a', href=True)


response = requests.get('https://www.mayoclinic.org' +
                        links[2]['href'], headers=headers)

response_soup = BeautifulSoup(response.content, "lxml")

doctor_specialty_list = response_soup.select_one('.result-items')
doctor_specialty_dict = list()
dc = set()

try:
    for i in range(1, 11):
        selection = doctor_specialty_list.select_one('.result-items > li:nth-child(' + str(
            i) + ') > div:nth-child(2) > ol:nth-child(2) > li:nth-child(1)').get_text().replace('\n', '')
        print(selection)
        doctor_specialty_dict.append(selection)
        dc = set(doctor_specialty_dict)
except Exception:
    pass

print(list(dc))
print(dir(doctor_specialty_dict))
